<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>NUnitLite</NuGetReference>
  <NuGetReference>Typemock.Isolator</NuGetReference>
  <Namespace>NUnit.Common</Namespace>
  <Namespace>NUnit.Framework</Namespace>
  <Namespace>NUnitLite</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>TypeMock.ArrangeActAssert</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

// 1. How would you improve the class MyApiSomething?
// 2. Write a few unit tests.
// 3. There are no right answers but this should provoke some thought,
// 		or at least fall under the category of "things that make you go hmph".

// Topics
// 1. async api design
// 2. proper usage of async/await
// 3. unit testing
// 4. dependency injection

// to use Typemock in LINQPad we need to set two system environment variables
// run these commands yourself from an admin prompt, then restart LINQPad
// SETX /M COR_PROFILER {B146457E-9AED-4624-B1E5-968D274416EC}
// SETX /M Cor_Enable_Profiling 0x1

async Task Main(string[] args)
{
	// force each run of this script to use a new process
	Util.NewProcess = true;

	// this will run the unit tests found in this script
	RunUnitTests();

	// begin here
	// comment out line #27 if you want to focus just on the unit tests
	CallMyApi();
}

private void CallMyApi()
{
	// begin here
	string[] sites =
	{
		"httpz://www.google.com",
		"https://www.asdfkj234kljrnvsdfs9f89swef.com",
		"https://www.google.com/aasdfasdfasdf"
	};

	var api = new MyApiSomething();

	foreach (string site in sites)
	{
		var result = api.Something(site);
		result.Dump(site);
	}
	// end here
}

public class MyApiSomething
{
	public MyApiSomething()
	{
		
	}
	
	/// <sumary>
	/// Get the text content of website.
	/// </summary>
	public async Task<object> Something(string url)
	{
		HttpClient http;

		try
		{
			http = new HttpClient();

			var response = http.GetAsync(url);

			var content = response.Result.Content.ReadAsStringAsync();
			return content;
		}
		catch (ArgumentException e)
		{
			throw new Exception(e.Message);
		}
	}
}

[TestFixture, Isolated]
public class MyApiSomethingTests
{
	[Test]
	public void TypemockIsActive()
	{
		var fakedate = new DateTime(1983, 6, 24);

		Isolate.WhenCalled(() => DateTime.Now).WillReturn(fakedate);

		Assert.AreEqual(fakedate, DateTime.Now);
	}
	
	[Test]
	public async Task httpclient_call_returns_404()
	{
		// arrange
		var http = Isolate.Fake.NextInstance<HttpClient>();
		Isolate.WhenCalled(() => http.GetAsync(default(string)))
			.DoInstead(context => {
				var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
				return Task.FromResult(resp);
			});
			
		// act
		var api = new MyApiSomething();
		Task<object> result = null;
		
		// assert
		Assert.DoesNotThrowAsync(async () => result = api.Something("invalidurl"));
		
		string resultString = result.Result.ToString();
		Assert.IsNull(resultString);
	}
}

// IGNORE THIS!
private void RunUnitTests()
{
	int execResult = new AutoRun().Execute(new[] { "-noheader", "-noresult" }, new ExtendedTextWrapper(Console.Out), Console.In);
	execResult.Dump("Unit Test Failures");
}