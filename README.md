# AcuCafe
C# Class library for application to Acumen
Refactored and extended code to supply requested additional functionality as well as facilitate future development.

- Stop the preparation of an ice tea with milk and inform the barista
Now validation has been added on the creation of drinks with condiments that are not valid for them, such as milk in ice tea of chocolate in any drink other than expresso. In such cases a command line message will be raised to inform the barista/customer of the reason for the failure in the order. Exception will also be thrown by the method, rather than return an invalid drink to the customer. The onus is on the calling method (waiter) to determine how to handle the exception (IE: Ask customer for another order)

- Add a new chocolate topping to the expresso
A new condiment type was added in the refactored structure. Updated flags and method signature to support this with an eye for backwards compatability. Price of chocolate was estimated at 0.7 to be distinct from milk and sugar prices.

Added unit tests to cover the functionality of the solution, including mocking out console and file path for the purpose of the test.

Comments added throughout code to explain and justify changes made.
