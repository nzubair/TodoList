﻿using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TodoList.Core.Models;
using Xunit;

namespace TodoList.UnitTests.Models
{
    public class TodoItemTest
    {
        [Fact]
        public void TodoItem_AllPropertiesCorrect_Ok()
        {
            var validationResults = new List<ValidationResult>();
            var todoItemId = Guid.NewGuid();
            var todoItem = new TodoItem()
            {
                Id = todoItemId,
                Title = "Cleaning",
                Content = "Clean the kitchen.",
                Added = new Instant(),
                DueTo = new Instant(),
#pragma warning disable CS0618 // 'TodoItem.AddedDateTime' is obsolete: 'Property only used for EF-serialization purposes'
                AddedDateTime = DateTime.Now,
#pragma warning restore CS0618 // 'TodoItem.AddedDateTime' is obsolete: 'Property only used for EF-serialization purposes'
#pragma warning disable CS0618 // 'TodoItem.DuetoDateTime' is obsolete: 'Property only used for EF-serialization purposes'
                DuetoDateTime = DateTime.Now,
#pragma warning restore CS0618 // 'TodoItem.DuetoDateTime' is obsolete: 'Property only used for EF-serialization purposes'
                Done = false,
                Tags = new[] { "test" },
                UserId = Guid.NewGuid().ToString(),
                File = new FileInfo()
                {
                    Path = "C:/temp",
                    Size = 100,
                    TodoId = todoItemId
                }
            };

            var isValid = Validator.TryValidateObject(todoItem, new ValidationContext(todoItem), validationResults);

            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void TodoItem_UserIdIsRequired()
        {
            var validationResults = new List<ValidationResult>();
            var todoItemId = Guid.NewGuid();
            var todoItem = new TodoItem()
            {
                Id = todoItemId,
                Title = "Cleaning",
                Content = "Clean the kitchen.",
                Added = new Instant(),
                DueTo = new Instant(),
#pragma warning disable CS0618 // 'TodoItem.AddedDateTime' is obsolete: 'Property only used for EF-serialization purposes'
                AddedDateTime = DateTime.Now,
#pragma warning restore CS0618 // 'TodoItem.AddedDateTime' is obsolete: 'Property only used for EF-serialization purposes'
#pragma warning disable CS0618 // 'TodoItem.DuetoDateTime' is obsolete: 'Property only used for EF-serialization purposes'
                DuetoDateTime = DateTime.Now,
#pragma warning restore CS0618 // 'TodoItem.DuetoDateTime' is obsolete: 'Property only used for EF-serialization purposes'
                Done = false,
                Tags = new[] { "test" },
                UserId = null, // Required
                File = new FileInfo()
                {
                    Path = "C:/temp",
                    Size = 100,
                    TodoId = todoItemId
                }
            };

            var isValid = Validator.TryValidateObject(todoItem, new ValidationContext(todoItem), validationResults);

            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
            Assert.Single(validationResults);
            Assert.Equal($"The {nameof(TodoItem.UserId)} field is required.", validationResults.Single().ErrorMessage);
        }
    }
}
