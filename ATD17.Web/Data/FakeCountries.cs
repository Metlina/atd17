namespace ATD17.Web.Data;

public static class FakeCountries
{
    public static List<DummyCountry> Countries { get; } = new List<DummyCountry>();

    public static void Initialize()
    {
        Countries.AddRange(new List<DummyCountry>
        {
            new()
            {
                Code = "HR",
                Id = 1,
                Name = "Croatia",
            },
            new()
            {
                Code = "BA",
                Id = 2,
                Name = "Bosnia and Herzegovina",
            },
            new()
            {
                Code = "SI",
                Id = 3,
                Name = "Slovenia",
            },
            new()
            {
                Code = "RS",
                Id = 4,
                Name = "Serbia",
            },
            new()
            {
                Code = "IT",
                Id = 5,
                Name = "Italy",
            }
        });
    }
}