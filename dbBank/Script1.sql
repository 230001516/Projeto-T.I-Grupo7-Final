--insert into dbo.Developers(id_developer, devName, devDescription, twitter, instagram, linkedin, devImage)
--values (1, 'Diogo', 'nigga', 'https://x.com/Baconcomovos1', 'https://www.instagram.com/diogo_baltazar/', 'd','assets/img/team/Diogao.jpg');


EXEC SP_DEVELOPERS_GET @id_developer = 1;