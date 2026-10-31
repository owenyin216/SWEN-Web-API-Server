DROP TABLE IF EXISTS user_favorites CASCADE;
DROP TABLE IF EXISTS rating_liked_by CASCADE;
DROP TABLE IF EXISTS rating CASCADE;
DROP TABLE IF EXISTS posts CASCADE;
DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS roles CASCADE;

create table roles(
  roles_id serial primary key, --serial ist autoincrement
  title varchar,
  description_of_the_role varchar
);

create table users (
  user_id serial primary key, 
  username  varchar not null unique,
  passwords varchar not null,
  roles_id int not null,
  created_at TIMESTAMPTZ, --timesstamp with utc
  email varchar,
  email_certified bool ,
  birthdate date ,
  ageverified bool, --fuer content moderation
  --location
  --gender
  --language
  
  foreign key (roles_id) references roles(roles_id)

    --on delete, remove from posts, ratings, likes, favourites
);

create table posts ( 
	--aka media entries
  post_id serial primary key ,
  title varchar ,
  description varchar, 
  body text, --the link to the media entry
  user_id integer not null,
  status varchar,
  created_at timestamp,

  foreign key (user_id) references users(user_id)

  --on delete, delete from post rating, likes, favourites 

);

create table rating(
	rating_id serial Primary key,
	rating_comment varchar null,
	user_id int not null,
	post_id int not null,
	rating int CHECK (rating BETWEEN 1 AND 5), --incluse 1 and 5
	UNIQUE (user_id, post_id),

	foreign key (user_id) references users(user_id),
  	foreign key (post_id) references posts(post_id)

	-- on delete, remove all likes

);

create table rating_liked_by(
	rating_id int not null,
	user_id int not null,

	foreign key (rating_id) references rating(rating_id),
  	foreign key (user_id) references users(user_id),

	UNIQUE (user_id, rating_id)

);

create table user_favorites(
	user_id int not null,
  	post_id serial not null,

	foreign key (post_id) references posts(post_id),
  	foreign key (user_id) references users(user_id),
	
	UNIQUE (user_id, post_id)

);



INSERT INTO roles (title, description_of_the_role) VALUES
('Admin', 'Systemadministrator mit vollen Rechten'),
('Moderator', 'Zuständig für Content Moderation'),
('User', 'Standardbenutzer');

INSERT INTO users (username, passwords, roles_id, created_at, email, email_certified, birthdate, ageverified) VALUES
('admin_01', 'hashed_password_1', 1, CURRENT_TIMESTAMP, 'admin@example.com', true, '1990-05-15', true),
('mod_anna', 'hashed_password_2', 2, CURRENT_TIMESTAMP, 'anna@example.com', true, '1995-10-20', true),
('max_mustermann', 'hashed_password_3', 3, CURRENT_TIMESTAMP, 'max@example.com', false, '2005-01-10', false);

INSERT INTO posts (title, description, body, user_id, status, created_at) VALUES
('Willkommen', 'Offizielle Begrüßung', 'Willkommen auf unserer neuen Plattform!', 1, 'published', CURRENT_TIMESTAMP),
('Community Regeln', 'Wichtig für alle', 'Bitte beachtet unsere Richtlinien für ein friedliches Miteinander.', 2, 'published', CURRENT_TIMESTAMP),
('Hallo Welt', 'Mein erster Beitrag', 'Ich bin neu hier und freue mich auf den Austausch.', 3, 'draft', CURRENT_TIMESTAMP);

-- Testdaten für die Tabelle 'rating'
INSERT INTO rating (rating_comment, user_id, post_id, rating) VALUES
('Sehr hilfreicher Beitrag!', 3, 1, 5),
('Gut geschrieben.', 1, 3, 4),
('Könnte mehr Details vertragen.', 2, 3, 3);

-- Testdaten für die Tabelle 'rating_liked_by'
INSERT INTO rating_liked_by (rating_id, user_id) VALUES
(1, 2), 
(2, 3); 

-- Testdaten für die Tabelle 'user_favorites'
INSERT INTO user_favorites (user_id, post_id) VALUES
(3, 1), 
(3, 2), 
(1, 3);

select * from users;
select * from roles;
select * from posts;
select * from rating;
select * from rating_liked_by;
select * from user_favorites;
