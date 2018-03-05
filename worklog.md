## Log
Development was test first, so the first goal was to make the exising tests pass.

* Project didn't run - EF had permission problems in SQLExpress.
	- Switched to using (localdb)
* Migrated project to core version of MVC and EF as I am more familiar with those
	- All initial test now pass
* Task 1 - Added CreditCard option
		 - Added tests that payment gateway is exercised correctly
* Task 2 - Modelled MenuItem as encapsulated part of MenuItemType
		 - Created Order model for index.cshtml, rendered course headers
* Task 3 - Charge correct price
* Task 4 - Show details of what was ordered
* Task 5 - Allow ordering multiple dishes

Notes about solution
* Basic clean architecture - the infrastructure depends on the domain logic, which has no dependencies
    - Often the domain logic would be in a separate assembly to highlight this, but this project is not complex enough
* The domain model contains encapsulated, (nearly) immutable classes
* The domain services provide ancillary functionality (e.g. processing payment)
* Project Features are self-contained groups of classes with their own view model
    - query and render the initial model
    - client modifies the model
    - server processes the modified model (places an order)
* Spiked a branch for view models that used real objects to model ordered dishes, rather than the IDs of the dishes.
    In this project I felt that it was over-complicated for the benefit.
* All hard-coded strings of type names have been eradicated, even (especially) in the cshtml
* Tests use randomly generated values and dependency injection
    - Keeps tests short and to the point
    - Each test has minimum setup required for that test case
    - Each test has one reason to fail
    - Helps tests to be supple in face of changes to codebase