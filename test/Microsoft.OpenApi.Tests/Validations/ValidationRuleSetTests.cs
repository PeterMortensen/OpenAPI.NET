﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Linq;
using Xunit;

namespace Microsoft.OpenApi.Validations.Tests
{
    public class ValidationRuleSetTests
    {
        [Fact]
        public void DefaultRuleSetReturnsTheCorrectRules()
        {
            // Arrange
            var ruleSet = new ValidationRuleSet();

            // Act
            var rules = ruleSet.Rules;

            // Assert
            Assert.NotNull(rules);
            Assert.Empty(rules);
        }

        [Fact]
        public void DefaultRuleSetPropertyReturnsTheCorrectRules()
        {
            // Arrange & Act
            var ruleSet = ValidationRuleSet.DefaultRuleSet;
            Assert.NotNull(ruleSet); // guard

            var rules = ruleSet.Rules;

            // Assert
            Assert.NotNull(rules);
            Assert.NotEmpty(rules);
            Assert.Equal(13, rules.ToList().Count); // please update the number if you add new rule.
        }
    }
}
