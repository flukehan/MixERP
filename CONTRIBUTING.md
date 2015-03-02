#Contributor License Agreement

A contributor needs to accept [MixERP Contributor License Agreement](https://github.com/mixerp/mixerp/blob/master/CLA.md) before we accept your pull request.

#How to Sign CLA?

Create a new pull request, adding a new file to the project. The file should be added to "/contributors" directory. The file name should be same as your GitHub username ending with ".md" extension. For example, for the GitHub user `alex`, the file would be `alex.md`. The file must have the following contents:

```
[date]

I hereby agree to the terms of the MixERP Contributors License
Agreement, as per the terms defined in the file "cla.md" in the root directory.

I furthermore declare that I am authorized and able to make this
agreement and sign this declaration.

Signed,

[your name]
https://github.com/[your github username]
```

Replace the text in square brackets as follows:

* Replace `[date]` with the AD date since when you want your agreement to be effective from. The format should be `YYYY-MM-DD` or `YYYY/MM/DD`.
* Replace `[your name]` with your name.
* Replace `[your github username]` with your GitHub username.


##Copyright Header
Each file (except *.designer.cs) must have the following license header.

```
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
```

##Dependency
Adding a new dependency needs to discussed with and approved by the repository owner (binodnp) beforehand.


#Coding Style

* Use 4 spaces instead of a tab.
* Namespaces, type names, member names, and parameter names must use complete words or standard abbreviations.


**Pascal Case**

* Function and Methods.
* Resource Strings (Except ScrudResource).
* Enumerators. Name and Collection.
* Class, Namespace.
* Properties.
* ASP.net Server Control and HTML Control ids.
* Javascript class objects and event names.

**Camel Case**

* Variables, Objects, and Parameters.


**Naming ASP.net Server Controls and Html Controls**

Names should be spelled in American English. The control id attribute should suffix the control name and type to suggest the type of control.

**Correct**

```
<asp:TextBox ID="FirstNameTextBox" runat="server" />

<input type="text" id="FirstNameInputText" runat="server" />

<asp:Button ID="SaveButton" runat="server" />

<input type="button" id="SaveInputButton" />

<button type="button" id="SaveButton" />

```

**Incorrect**

```
<asp:TextBox ID="txt_FirstName" runat="server" />

<input type="text" id="FirstNameTB" runat="server" />

<asp:Button ID="SaveBtn" runat="server" />

<input type="button" id="Save_Button" />

<button type="button" id="BtnSave" />

```

##Other Important Rules to Follow
* Functions and methods names must always begin with verbs.
* Class names should be singular nouns.
* Collection, array, list, IEnumerable and similar objects should be plural.
* Controls implementing IDisposable should always remove event handlers. Please do not set 
  event delegate to null on Dispose() method.
