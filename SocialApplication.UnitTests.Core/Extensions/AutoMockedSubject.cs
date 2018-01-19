using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace SocialApplication.UnitTests.Core.Extensions
{
    public abstract class AutoMockedSubject<TSubjectToTest>
        where TSubjectToTest : class 
    {
        private AutoMocker _autoMocker;

        private TSubjectToTest _subjectToTest;

        protected TSubjectToTest Sub
        {
            get
            {
                if (_subjectToTest != null)
                {
                    return _subjectToTest;
                }

                _subjectToTest = _autoMocker.CreateInstance<TSubjectToTest>();
                return _subjectToTest;
            }
        }

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
        }

        [TearDown]
        public void TearDown()
        {
            _subjectToTest = null;
        }

        protected Mock<TDependencyType> Use<TDependencyType>() 
            where TDependencyType : class
        {
            return _autoMocker.GetMock<TDependencyType>();
        }
    }
}
