USE [TransactionDB]
GO

/****** Object:  Table [dbo].[tbl_Transaction]    Script Date: 04/26/2022 6:12:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_Transaction](
	[TransactionId] [varchar](50) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[CurrencyCode] [varchar](3) NOT NULL,
	[TranscationStatus] [varchar](10) NOT NULL,
	[TranscationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


