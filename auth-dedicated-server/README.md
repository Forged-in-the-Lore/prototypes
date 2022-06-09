# Authentication Service Prototype

This is a prototype project for some research I did about authentication with multiple services.

It uses a version of my [authentication service](https://github.com/davidhellinga/forgedinthelore-net) with extended funcionality that allows it to internally verify tokens. A python fastapi server uses that endpoint to verify the bearer token of the user. This can be tested with postman.

## Usage

1. Start and seed the forgedinthelore-net auth service by running "make create-dev-db" and "make seed" (in its own folder)
2. Start the python auth-consumer-api with "make up" (in its own folder)
3. Import the postman collection
4. Run the Login request
5. Run test requests
