using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole.Tests.Assets
{
    public class AutoNDataAttribute : AutoDataAttribute
    {
        public AutoNDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoNSubstituteCustomization() {
                    ConfigureMembers = true
                })) {

        }
        protected AutoNDataAttribute(IFixture fixture)
            : base(() => fixture.Customize(new AutoNSubstituteCustomization())) {

        }
    }
}
