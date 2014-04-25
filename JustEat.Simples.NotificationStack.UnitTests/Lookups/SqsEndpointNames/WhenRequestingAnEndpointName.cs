﻿using JustSaying.AwsTools.QueueCreation;
using JustSaying.Messaging;
using JustSaying.Stack;
using JustSaying.Stack.Lookups;
using JustEat.Testing;
using NSubstitute;
using NUnit.Framework;

namespace Stack.UnitTests.Lookups.SqsEndpointNames
{
    public class WhenRequestingAnEndpointName : BehaviourTest<SqsSubscribtionEndpointProvider>
    {
        private readonly IMessagingConfig _publishConfig = Substitute.For<IMessagingConfig>();
        private readonly SqsConfiguration _sqsConfiguration = new SqsConfiguration();

        private string _result;

        protected override SqsSubscribtionEndpointProvider CreateSystemUnderTest()
        {
            return new SqsSubscribtionEndpointProvider(_sqsConfiguration, _publishConfig);
        }

        protected override void Given()
        {
            _publishConfig.Environment.Returns("QAxx");
            _publishConfig.Tenant.Returns("OuterHebredies");
            _publishConfig.Component = "BoxHandler";

            _sqsConfiguration.Topic = "OrderDispatch";
        }

        protected override void When()
        {
            _result = SystemUnderTest.GetLocationName();
        }

        [Then]
        public void LocationIsBuiltInCorrectStructure()
        {
            Assert.AreEqual("outerhebredies-qaxx-boxhandler-orderdispatch", _result);
        }
    }
}