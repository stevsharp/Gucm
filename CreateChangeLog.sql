CREATE TABLE [dbo].[ChangeLog](
	[ChangeLogID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](255) NOT NULL,
	[PropertyName] [varchar](255) NOT NULL,
	[Key] [varchar](255) NOT NULL,
	[OriginalValue] [varchar](max) NULL,
	[CurrentValue] [varchar](max) NULL,
	[UserName] [varchar](25) NOT NULL,
	[ChangeDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ChangeLog] PRIMARY KEY CLUSTERED 
(
	[ChangeLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ChangeLogExclusion](
	[ChangeLogExclusionID] [int] IDENTITY(1,1) NOT NULL,
	[EntityName] [varchar](128) NOT NULL,
	[PropertyName] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ChangeLogExclusion] PRIMARY KEY CLUSTERED 
(
	[ChangeLogExclusionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [U_ChangeLogExclusion] UNIQUE NONCLUSTERED 
(
	[EntityName] ASC,
	[PropertyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO