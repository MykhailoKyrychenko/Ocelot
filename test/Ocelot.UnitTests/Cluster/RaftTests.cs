using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Ocelot.Cluster.Raft;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class RaftTests
    {
        private Node _node;
        private FakeMessageBus _messageBus;
        
        [Fact]
        public void nodes_should_state_in_follower_state()
        {
            this.When(x => WhenICreateANode())
                .Then(x => x.ThenItsStateIsFollower())
                .And(x => ThenTheCurrentTermIs(0))
                .And(x => ThenTheNodeHasAnId())
                .BDDfy();
        }

        [Fact]
        public void node_should_reply_with_vote_if_receives_vote_request()
        {
            this.Given(x => GivenICreateANode())
                .When(x => WhenANodeRecievesAVoteRequest(new RequestVote(Guid.NewGuid())))
                .Then(x => x.ThenTheNodeRepliesWithItsVote())
                .BDDfy();
        }

        [Fact]
        public void node_should_become_candidate_if_does_not_hear_from_leader()
        {
            this.Given(x => GivenICreateANode())
                .When(x => WhenTheNodeDoesNotHearFromTheLeader(new BecomeCandidate()))
                .Then(x => x.ThenTheNodeBecomesACandidate())
                .And(x => ThenTheCurrentTermIs(1))
                .BDDfy();
        }

        [Fact]
        public void node_should_request_votes_if_becomes_candidate()
        {
            this.Given(x => GivenICreateANode())
                .When(x => WhenTheNodeDoesNotHearFromTheLeader(new BecomeCandidate()))
                .Then(x => ThenTheNodeRequestsVotes())
                .And(x=> ThenTheNodeVotesForItself())
                .BDDfy();
        }

        private void ThenTheNodeVotesForItself()
        {
            var requestVote = (RequestVote)_messageBus.Messages[0];
            requestVote.FromId.ShouldBe(_node.Id);
        }

        private void ThenTheCurrentTermIs(int expected)
        {
            _node.CurrentTerm.ShouldBe(expected);
        }

        private void WhenANodeRecievesAVoteRequest(RequestVote requestVote)
        {
            _node.Handle(requestVote);
        }

        private void ThenTheNodeRepliesWithItsVote()
        {
            _messageBus.Messages[0].ShouldBeOfType<Vote>();
        }

        private void ThenTheNodeRequestsVotes()
        {
            _messageBus.Messages[0].ShouldBeOfType<RequestVote>();
        }

        private void GivenICreateANode()
        {
            WhenICreateANode();
        }

        private void WhenTheNodeDoesNotHearFromTheLeader(BecomeCandidate becomeCandidate)
        {
            _node.Handle(becomeCandidate);
        }

        private void ThenTheNodeHasAnId()
        {
            _node.Id.ShouldNotBeNull();
        }

        private void ThenTheNodeBecomesACandidate()
        {
           _node.State.ShouldBeOfType<Candidate>();
        }

        private void WhenICreateANode()
        {   
            _messageBus = new FakeMessageBus();
            _node = new Node(_messageBus);
        }

        private void ThenItsStateIsFollower()
        {
            _node.State.ShouldBeOfType<Follower>();
        }

        class FakeMessageBus : IMessageBus
        {
            public FakeMessageBus()
            {
                Messages = new List<IMessage>();
            }
            
            public List<IMessage> Messages {get;private set;}
            
            public void Publish(IMessage message)
            {
                Messages.Add(message);
            }
        }
    }
}