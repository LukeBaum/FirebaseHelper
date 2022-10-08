NOTE: This project is ancient and will be archived as the Firebase platform has changed entirely.

FirebaseHelper
==============

A .NET library that allows you to easily read, write, and delete data from a Firebase.

This version of the library comes with a simple
Helpers.Firebase.FirebaseHelper object with 5 different functions.

Using the object is quite simply.

Initialize like so: Dim FbHelper As New
FirebaseHelper("FirebaseName","MyFirebaseSecretOrAuthTokenHere")

Then you can use any of the 5 methods. For example:
FbHelper.PushData("users/3240/name","{""first"":""Chuck"",""last"":""Norris""}")

In the above case, the name of the newly pushed child node is returned.
Nothing is returned in the case of a failure. The other methods return
an enumeration member, or in the case of the ReadData function, the data
returned (or Nothing on an error).

Because this library is so small, I will refer you to the source code to see what's going on.

If you ever need to pass data (this is the case for WriteData, PushData, and UpdateData) you
will need to send it in JSON format. Expect an error if your JSON is malformed.

This library accesses your Firebase through Firebase's REST API. Documentation on these
underlying calls can be found here: https://www.firebase.com/docs/rest-api.html
