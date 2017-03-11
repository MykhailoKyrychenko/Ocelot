using System;
using System.Collections.Generic;
using System.Threading;

namespace Ocelot.Cluster.Raft
{
    public class Node
    {
        private IMessageBus _messageBus;
        private IGetAllNodes _getAllNodes;

        public Node(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            State = new Follower();
            CurrentTerm = 0;
            Id = Guid.NewGuid();
            var becomeCandidate = new BecomeCandidate();
            var listenForLeader = new SendToSelfWithDelay(becomeCandidate, 3);
        }

        public void Handle(BecomeCandidate becomeCandidate)
        {
            State = new Candidate();
            CurrentTerm++;

            //start voting and vote for self
            _messageBus.Publish(new RequestVote(Id));
        }

        public void Handle(RequestVote requestVote)
        {
            _messageBus.Publish(new Vote());
        }

        public int CurrentTerm { get; private set; }
        public State State { get; private set; }
        public Guid Id { get; private set; }
    }

    public abstract class State
    {

    }

    public class Follower : State
    {

    }

    public class Candidate : State
    {

    }


    public class Leader : State
    {

    }

    public interface IMessageBus
    {
        void Publish(IMessage message);
    }

    public class IMessage
    {
    }

    public class SendToSelfWithDelay : IMessage
    {
        public SendToSelfWithDelay(IMessage message, int delaySeconds)
        {
            Message = message;
            DelaySeconds = delaySeconds;
        }

        public IMessage Message { get; }
        public int DelaySeconds { get; }
    }

    public class BecomeCandidate : IMessage
    {

    }

    public class RequestVote : IMessage
    {
        public RequestVote(Guid fromNodeId)
        {
            this.FromId = fromNodeId;

        }
        public Guid FromId { get; private set; }
    }

    public class Vote : IMessage
    {

    }

    public class Heartbeat : IMessage
    {

    }

    public class GetAllNodes : IGetAllNodes
    {
        public List<NodeLocation> Get()
        {
            throw new NotImplementedException();
        }
    }

    public interface IGetAllNodes
    {
        List<NodeLocation> Get();
    }

    public class NodeLocation
    {
        public NodeLocation(Guid id, Uri address, string name)
        {
            Id = id;
            Address = address;
            Name = name;

        }

        public Guid Id { get; private set; }
        public Uri Address { get; private set; }
        public string Name { get; private set; }
    }
}