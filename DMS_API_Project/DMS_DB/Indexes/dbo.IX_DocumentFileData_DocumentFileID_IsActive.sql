CREATE UNIQUE INDEX [IX_DocumentFileData_DocumentFileID_IsActive]
    ON [dbo].[DocumentFileData]([DocumentFileID])
    WHERE [IsActive] = 1