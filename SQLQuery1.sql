
-- Insert data into the User table with Firebase user IDs
INSERT INTO [User] (Username, Password, FullName, CreateTime, FirebaseUserId)
VALUES
  ('robert.johnson@example.com', 'password3', 'Robert Johnson', '2023-07-26', '4UG4jdmzIfbBDYr3qBXwQroZi6t1'),
  ('jane.smith@example.com', 'password2', 'Jane Smith','2023-07-26', 'ctMIT435CxQYOp1P2yftYGEYCor1'),
  ('john.doe@example.com', 'password1', 'John Doe','2023-07-25', 'PrlSBsX4OJe8z8BN4gt0gHrVPeX2');

-- Insert data into the CodeSnippet table
INSERT INTO CodeSnippet (UserId, Title, Content, Description, CreateTime, CreatedBy)
VALUES
  (1, 'Snippet 1', 'console.log("Hello, World!");', 'Prints "Hello, World!" to the console.', '2023-07-26', 'robert.johnson@example.com'),
  (2, 'Snippet 2', 'function add(a, b) { return a + b; }', 'Adds two numbers and returns the sum.', '2023-07-26', 'jane.smith@example.com'),
  (3, 'Snippet 3', 'const name = "John";', 'Defines a constant variable "name" with the value "John".', '2023-07-25', 'john.doe@example.com');

-- Insert data into the FavoriteSnippet table
INSERT INTO FavoriteSnippet (UserId, SnippetId, CreateTime)
VALUES
  (1, 2, '2023-07-28'),
  (2, 3, '2023-07-27'),
  (3, 1, '2023-07-26');

-- Insert data into the Tag table
INSERT INTO Tag (Name, Category)
VALUES
  ('JavaScript', 'Programming'),
  ('React', 'Framework'),
  ('HTML', 'Markup');

-- Insert data into the CodeSnippetTag table
INSERT INTO CodeSnippetTag (SnippetId, TagId)
VALUES
  (1, 1),
  (1, 2),
  (2, 2),
  (3, 1),
  (3, 3);

-- Insert data into the UserPreference table
INSERT INTO UserPreference (UserId, BackgroundColor, FontColor, FontSize)
VALUES
  (1, '#f0f0f0', '#333', 16),
  (2, '#ffffff', '#000', 14),
  (3, '#dcdcdc', '#444', 18);

-- Insert data into the BackgroundSlideshow table
INSERT INTO BackgroundSlideshow (ImageUrl, Title)
VALUES
  ('https://example.com/image1.jpg', 'Image 1'),
  ('https://example.com/image2.jpg', 'Image 2'),
  ('https://example.com/image3.jpg', 'Image 3');
