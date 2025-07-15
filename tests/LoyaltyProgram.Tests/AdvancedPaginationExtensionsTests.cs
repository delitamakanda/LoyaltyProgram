using System.Linq.Expressions;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Tests;

public class AdvancedPaginationExtensionsTests
{
    [Fact]
    public async Task ToPagedResultAsync_ReturnsCorrectPageInfo()
    {
        // Arrange
        var testData = new List<TestItem>
    {
        new TestItem { Id = 1, Name = "Item 1" },
        new TestItem { Id = 2, Name = "Item 2" },
        new TestItem { Id = 3, Name = "Item 3" },
        new TestItem { Id = 4, Name = "Item 4" },
        new TestItem { Id = 5, Name = "Item 5" }
    }.AsQueryable();

        int page = 2;
        int pageSize = 2;

        // Act
        var result = await testData.ToPagedResultAsync(page, pageSize);

        // Assert
        Assert.Equal(page, result.Page);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(testData.Count(), result.TotalCount);
        Assert.Equal(3, result.TotalPages); // 5 items with 2 per page = 3 pages
        Assert.Equal(2, result.Items.Count); // 2 items on page 2
        Assert.Equal(3, result.Items[0].Id); // First item on page 2 should be Item 3
        Assert.Equal(4, result.Items[1].Id); // Second item on page 2 should be Item 4
    }

    [Fact]
    public async Task ToPagedResultAsync_FiltersCorrectly_WhenDateRangeProvided()
    {
        // Arrange
        var testData = new List<DateTestItem>
        {
            new DateTestItem { Id = 1, Name = "Item 1", CreatedDate = new DateTime(2023, 1, 1) },
            new DateTestItem { Id = 2, Name = "Item 2", CreatedDate = new DateTime(2023, 2, 15) },
            new DateTestItem { Id = 3, Name = "Item 3", CreatedDate = new DateTime(2023, 3, 20) },
            new DateTestItem { Id = 4, Name = "Item 4", CreatedDate = new DateTime(2023, 5, 10) },
            new DateTestItem { Id = 5, Name = "Item 5", CreatedDate = new DateTime(2023, 7, 30) }
        }.AsQueryable();

        var startDate = new DateTime(2023, 2, 1);
        var endDate = new DateTime(2023, 6, 1);
        Expression<Func<DateTestItem, DateTime?>> dateSelector = item => item.CreatedDate;

        // Act
        var result = await testData.ToPagedResultAsync(
            page: 1,
            pageSize: 10,
            startDate: startDate,
            endDate: endDate,
            dateSelector: dateSelector);

        // Assert
        Assert.Equal(3, result.Items.Count);
        Assert.Equal(2, result.Items[0].Id); // Item with date 2023-02-15
        Assert.Equal(3, result.Items[1].Id); // Item with date 2023-03-20
        Assert.Equal(4, result.Items[2].Id); // Item with date 2023-05-10
        Assert.DoesNotContain(result.Items, item => item.Id == 1); // Before start date
        Assert.DoesNotContain(result.Items, item => item.Id == 5); // After end date
    }

    public class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DateTestItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
