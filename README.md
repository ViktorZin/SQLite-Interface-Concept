# SQLite Integration with Self-Managing Data Classes
This is my concept for an SQLite integration where each class reads and writes its own data to and from the database.
The goal was to make the codebase more modular and easier to manage.


# Scripts Overview
## Data.cs
This is the parent class of Country.cs and Spell.cs.
Its sole purpose is to provide a common base class for all data types, allowing them to be handled uniformly (e.g., in a List<Data> or Dictionary<string, Data>).
Casting can be used later to access child-specific data.

## Spell.cs, Country.cs
Both inherit from Data.cs. These serve as example implementations of the system.
Spell.cs is the most complete and serves as the best reference.

## IDBReadable.cs, IDBWritable.cs
These are the interfaces used by the system to convert between SQLite table rows and class instances.

## DBHandler.cs
Handles the connection to the SQLite database and provides read and write methods for interacting with it.

## DataHolder.cs
Stores all the data as class instances (in lists and dictionaries).
Also triggers the initial data loading during Start().

# Additional Notes
The scripts are designed for use in the Unity3D Game Engine.
Unity methods like Awake() and Start() are triggered automatically when the game or application starts — in that order.

The DataHolder.cs script calls ReadDB() to load data from the database and stores it in appropriate structures.

# Data Flow Details
DBHandler.cs expects each class to implement the IDBReadable interface.
Each row from the database (SQLiteDataReader) is an IDataRecord, which can be read individually.
The record is passed to a newly instantiated class, which implements the InterpretDBData(IDataRecord record) method from IDBReadable.
This design cleanly hands off a table row to the corresponding class, which is responsible for assigning the data to its fields and properties.

For writing:
The IDBWritable interface defines a WriteClassToDB() method.
This method is implemented per class and converts its fields/properties into a list of strings to be written back into the database.

# Challenges
Currently, nested or relational data structures are not well supported by this system.

Example:
In DataHolder.cs, I maintain dictionaries for Settlements and Buildings.
These should ideally be embedded inside the Country class.
When loading such nested data, custom handling would be required to associate and reconstruct the full hierarchy.

# Advantages of This Approach
The key benefit of this system — or at least the core idea — is separation of concerns:
- No monolithic script that contains all database logic.
- Each class defines its own read/write logic, making the code easier to maintain.
- Debugging is simplified, as you always know which class to look at when something goes wrong.