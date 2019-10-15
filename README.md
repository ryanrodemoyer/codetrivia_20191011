# Readme

1. How would you improve the class MyApiSomething?
2. Write a few unit tests.
3. There are no right answers but this should provoke some thought or at least fall under the category of "things that make you go hmph".

# Participating
Fork my repo and send a PR by the end of October 21st (Monday).

# This is really cool
With a single LINQPad script you can:
* write functional code
* and/or, reference arbitary assemblies
* write and run unit tests with NUnit (via NUnitLite) **and Typemock**!

# Topics
1. async api design
2. proper usage of async/await
3. unit testing
4. dependency injection

## Typemock Configuration for LINQPad
To use Typemock in LINQPad we need to set two system environment variables

run these commands yourself from an admin prompt, then restart LINQPad

`SETX /M COR_PROFILER {B146457E-9AED-4624-B1E5-968D274416EC}`

`SETX /M Cor_Enable_Profiling 0x1`
