--ALTER TABLE [dbo].[Post] drop CONSTRAINT [PK_Post]
--ALTER TABLE [dbo].[Post] drop CONSTRAINT [FK_Post_Related_Id]
--ALTER TABLE [dbo].[Related] drop CONSTRAINT [PK_Related]
--ALTER TABLE [dbo].[Related] drop CONSTRAINT [AK_Related_UserId]
--ALTER TABLE [dbo].[Related] drop CONSTRAINT [AK_Related_ParentId]
--ALTER TABLE [dbo].[Related] drop CONSTRAINT [FK_Related_Related_CurrentId]
--Drop INDEX [IX_Related_ParentId] ON [dbo].[Related]
--Drop INDEX [IX_Related_UserId] ON [dbo].[Related]


ALTER TABLE [dbo].[Post] ADD CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] ASC);
ALTER TABLE [dbo].[Post] ADD CONSTRAINT [FK_Post_Related_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Related] ([CurrentId]);
ALTER TABLE [dbo].[Related] ADD CONSTRAINT [PK_Related] PRIMARY KEY CLUSTERED ([CurrentId] ASC)
ALTER TABLE [dbo].[Related] ADD CONSTRAINT [AK_Related_UserId] UNIQUE NONCLUSTERED ([UserId] ASC)
ALTER TABLE [dbo].[Related] ADD CONSTRAINT [AK_Related_ParentId] UNIQUE NONCLUSTERED ([ParentId] ASC)
ALTER TABLE [dbo].[Related] ADD CONSTRAINT [FK_Related_Related_CurrentId] FOREIGN KEY ([CurrentId]) REFERENCES [dbo].[Related] ([ParentId])
CREATE UNIQUE NONCLUSTERED INDEX [IX_Related_ParentId] ON [dbo].[Related]([ParentId] ASC);
CREATE UNIQUE NONCLUSTERED INDEX [IX_Related_UserId] ON [dbo].[Related]([UserId] ASC);