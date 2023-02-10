# SSH-TicTacToe

To run the API you have to have .Net 6 installed in your machine and VS 2022

A python Fast API version from this, is from here as well: [FastAPI Tic Tac Toe](https://github.com/rhourani/FastAPI/tree/Tic-tac-toe-)

Use https://editor.swagger.io/ to see the swagger.yaml api file

A quick loom video https://www.loom.com/share/5f7cdd9d48bb4ba18446c2e6625b8dc9

Screenshot
<img src="API screenshot.png" align="center">


# Used Techs:
* .Net
* SOLID
* TDD
* Domain Driven Design Pattern / Clean architecture
* Test Driven Desgin

#NEW
# Parsing authorize_keys file endpoints
* Read file,
* List keys

# Added new tech stack
* EF 6

<img src="AuthorizedKeys API result.png" align="center">

#SQL query to search keys contains specific option 
SELECT * FROM [SSH].[dbo].[AuthorizedKeys] where Options like '%no-port-forwarding%'

<img src="SQL query.png" align="center">


#Futher improvemnts
Depending on the usage and requirments of the application the authrized keys can be used mainly as a hub for authentication.
The db fields can be extended to meet these expectations and requirments.

On the technical side of the whole app many improvemnts can be done. Adding user authentication and policy based authorizations, integrate the solution with cloud technolgy. Enhance the security of the API (needs me to read more about it). The internal structure of the endpoints seems a bit unorganized. Error handling and more SOLID principles can be followed.
Convert the DB connection to dependency injection is recommended.
