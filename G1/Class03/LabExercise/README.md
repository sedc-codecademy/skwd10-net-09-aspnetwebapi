PHONE BOOK LAB EXERCISE

Task One:

Create Web API application that supports endpoint for ADDING, REMOVING, READING and UPDATING contacts in a phone book.
We wont use any fancy architecture, neather real database. We are going to use StaticDB.

Suggested methods:

- CreateContact
- GetAllContacts
- GetContactById
- DeleteContact
- UpdateContact

The contact domain model should have the following properties:

- Id - int
- FullName - string
- PhoneNumber - string
- HasViber - boolean

Task Two:

Make aditional endpoints that will support filtering by:

- Full name property
- HasViber propery

Bonus: 

Make one aditional endpoint that will generate a secret key, (can be hardcoded string, some facy randomizer or guid generator) and store that key in to the database.
Secure all endpoints with taht key, so a client cant access the api if the correct key is not provided as QUERY parameter.
TIP: You will also need an endpoint for getting the key. :)




