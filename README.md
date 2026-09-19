Critical Hit or Miss: My Gaming Web Application

Overview:
My Game Admin. App is a website application that allows administrators to manage a catalog of games, including creating, viewing, updating, and deleting game records, as well as managing how the website performs.

This project is being built as part of my INET2005 Web Application Program, with a focus on applying CRUD principles, a clean architecture, and good software engineering practices through databases.

This app is only one piece of a broader gaming platform architecture:
-> This app: Used internally by staff/the administration to add new game releases, with their correct metadata, ranging from titles, genres, platforms, release dates, ratings, and pricings
-> Removing outdated or delisted titles from storefronts on the site itself, just to keep it up to date.
-> Shared Database/API: The single source of data/information that both this admin app and any reviewer facing app will read from and write to, ensuring consistency across all systems/databanks.
-> Reviewer Facing App: Where our users, mostly reviewers, will search, browse, and view game details, reviews, or store listings. It will ONLY read data, all creation and editing happens here inside this admin app.

In short, this repo is the administrative part of the website that powers the rest of the platform I will be making throughout this course.

Features for the admin side and eventually the reviewer side:
Admin Side:
-> Add new games with title, genre, platform, description, release date, and ratings
-> Edit existing game records
-> Delete games from the catalog

Reviewer Side:
-> Search/filter the game list
-> View a full list of all games in the system with reviews and ratings applied
