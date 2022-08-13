PHONEBOOK LAB EXERCISE

Task One:

Create Web api application that supports endpoint for ADDING,RREMOVING, READING and UPDATING records in a phonebook.
We wont use any fancy architecture, neather real database. We are going to use StaticDB.

Suggested methods:

- CreateRecord
- GetAllRecords
- GetRecordById
- DeleteRecord
- UpdateRecord

The record domain model should have the following properties:

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
TIP: You will also need an enpoint for getting the key. :)


