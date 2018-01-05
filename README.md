![alt text](https://github.com/tonygeorgiew/Bulgarian-Apparel/blob/master/Preview/sizes.png)
![alt text](https://github.com/tonygeorgiew/Bulgarian-Apparel/blob/master/Preview/adminn.png)
![alt text](https://github.com/tonygeorgiew/Bulgarian-Apparel/blob/master/Preview/widget.png)
![alt text](https://github.com/tonygeorgiew/Bulgarian-Apparel/blob/master/Preview/categories.png)
![alt text](https://github.com/tonygeorgiew/Bulgarian-Apparel/blob/master/Preview/datatable.png)
![alt text](https://github.com/tonygeorgiew/Bulgarian-Apparel/blob/master/Preview/categories.png)

# Bulgarian-Apparel
E-Commerce clothing shop MVC web application with 3-Tier architecture along with dependecy injection and repository pattern.
-
*Presentation Layer
Shop categorization with equal height divs for each listed shop item.
Carrusel widget.
Detailed data table order view with sorting and searching.
Product image viewer with timed view timeout on each image.
-
*Business Layer
public part (accessible without authentication) - user shop search and product view
private part (available for registered users) - user's profiles management functionality, user's shop order etc.
administrative part (available for administrators only) -  administrative access to the system and permissions to administer all major information objects in the system, e.g. to create/edit/delete users and other administrators(ASP.NET Identity System), to edit/delete products and categories in an e-commerce system, etc.
-
*Data Layer
RDBMS(MS SQL).
Entity Framework 6 + Repository Pattern with additional Service layer.
-



TO DO List:
Refractoring and more unit tests.
