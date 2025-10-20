USE [WorkFlow2]
GO

/****** Object:  Table [dbo].[LinkCondation]    Script Date: 9/7/2024 11:21:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LinkCondation](
	[LinkCondationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplictionLinkId] [int] NOT NULL,
	[ActionID] [int] NOT NULL,
	[AnyOne] [bit] NOT NULL,
	[MustAll] [bit] NOT NULL,
	[ChangeActionID] [int] NULL,
 CONSTRAINT [PK_LinkCondation] PRIMARY KEY CLUSTERED 
(
	[LinkCondationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LinkCondation]  WITH CHECK ADD  CONSTRAINT [FK_LinkCondation_Actions] FOREIGN KEY([ActionID])
REFERENCES [dbo].[Actions] ([ActionID])
GO

ALTER TABLE [dbo].[LinkCondation] CHECK CONSTRAINT [FK_LinkCondation_Actions]
GO

ALTER TABLE [dbo].[LinkCondation]  WITH CHECK ADD  CONSTRAINT [FK_LinkCondation_ActionsForChange] FOREIGN KEY([ChangeActionID])
REFERENCES [dbo].[Actions] ([ActionID])
GO

ALTER TABLE [dbo].[LinkCondation] CHECK CONSTRAINT [FK_LinkCondation_ActionsForChange]
GO

ALTER TABLE [dbo].[LinkCondation]  WITH CHECK ADD  CONSTRAINT [FK_LinkCondation_ApplicationLink] FOREIGN KEY([ApplictionLinkId])
REFERENCES [dbo].[ApplicationLink] ([ApplictionLinkId])
GO

ALTER TABLE [dbo].[LinkCondation] CHECK CONSTRAINT [FK_LinkCondation_ApplicationLink]
GO


