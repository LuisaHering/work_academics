## Work Academics

## Popular tabela `ROLE`

``` script

insert 
into		dbo.Roles   	(Descricao)
values				('ADMIN'),
				('COORDENADOR'),
				('DOUTORANDO'),
				('MESTRANDO'),
				('IC')

```

## Adicionar STORED PROCEDURE AO BANCO DE DADOS
``` script

CREATE PROCEDURE [dbo].[Publicacoes]
	@id_user as nvarchar(max)
AS
select			*
from			dbo.Posts	as	p

where			( 1 = 1 )

--	posts do usuario
	and		p.[Autor_Id] = @id_user		

--	posts dos amigos
	or		p.[Autor_Id] 
				in 	(
						select		Seguido_Id
						from		dbo.Conections		as c
						where		c.Seguidor_Id = @id_user			
					)

--	posts dos membros do laboratorio
	or		p.[Autor_Id]
				in	(
						select		lu.User_Id
						from		dbo.Laboratories				as l
								left join	dbo.LaboratoryUsers		as lu
										on	l.Id = lu.Laboratory_Id	
						where		( 1 = 1)
								and	lu.User_Id <> @id_user
					)
RETURN 0

```

## ADICIONAR STORED PROCEDURE AO BANCO DE DADOS
``` script

CREATE PROCEDURE [dbo].[Conexoes]
	@id_user as nvarchar(max)
as
select		*
from		dbo.Users						as u
where		(1=1)
and		u.[Id] in 	(
					select	c.[Seguido_Id]
					from	dbo.[Conections]	as c
					where	(1=1)
					and	c.[Seguidor_Id] = @id_user
				)

```
