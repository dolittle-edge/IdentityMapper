using TechTalk.SpecFlow;
using IdentityMapper.Specs.Drivers;

namespace IdentityMapper.Specs.StepDefinitions
{
    [Binding]
    public class Steps
    {
        private readonly Driver _driver;
        private readonly TimeSeriesMapper _mapper;

        public Steps(Driver driver, TimeSeriesMapper mapper)
        {
            _mapper = mapper;
            _driver = driver;
        }

        [Given(@"Put your Background here")]
        public void GivenPutYourBackgroundHere()
        {
            _driver.CreateBackground();
        }

        [When(@"Put your Action here")]
        public void WhenPutYourActionHere()
        {
            _driver.ExecuteAction();
        }

        [Then(@"Put your Condition here")]
        public void ThenPutYourConditionHere()
        {
            _driver.CheckCondition();
        }
    }
}
