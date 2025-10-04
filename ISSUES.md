# Project Issues and TODOs

## Database Migration Cleanup

**Status:** Open  
**Priority:** Medium  
**Created:** 2025

### Issue: Drop Old Database Migrations

#### Description
The project currently contains several database migration files, including an empty migration that should be cleaned up. The old database structure needs to be reviewed and potentially dropped/consolidated.

#### Affected Files
- `Data/Migrations/20251001215438_mssql.local_migration_671.cs` - Empty migration file with no Up or Down operations
- Other migration files may need review for consolidation

#### Current Migration Timeline
1. `00000000000000_CreateIdentitySchema.cs` - Initial Identity setup
2. `20250928145631_mssql.local_migration_170.cs` - Creates Worker, Project, Activity, Comment, and ProjectWorker tables
3. `20251001215438_mssql.local_migration_671.cs` - **Empty migration** (no operations)
4. `20251004001105_UpdateDB.cs` - Updates Activity table relationships and adds new columns

#### Recommendations

1. **Remove Empty Migration**
   - The migration `20251001215438_mssql.local_migration_671.cs` is empty and serves no purpose
   - Should be safely removed from the project as it doesn't modify the database schema

2. **Consider Migration Consolidation**
   - If starting fresh or resetting development database, consider:
     - Squashing multiple migrations into a single initial migration
     - Creating a clean baseline migration that represents the current state
     - Removing migration history if not in production

3. **Database Cleanup Steps**
   ```bash
   # Option 1: Remove empty migration file
   # Delete Data/Migrations/20251001215438_mssql.local_migration_671.cs
   # Delete Data/Migrations/20251001215438_mssql.local_migration_671.Designer.cs
   
   # Option 2: Drop and recreate database (development only)
   dotnet ef database drop
   dotnet ef database update
   
   # Option 3: Create fresh migration baseline (if not in production)
   # Remove all migration files
   # Remove Migrations table from database
   # Run: dotnet ef migrations add InitialCreate
   ```

#### Notes
- Before dropping any database or migrations, ensure you have backups
- Coordinate with team members if database is shared
- This cleanup is recommended for development environments
- Production environments require careful migration planning

#### Action Items
- [ ] Review with team whether database is in production use
- [ ] Decide on cleanup approach (remove empty migration vs full reset)
- [ ] Execute cleanup in development environment first
- [ ] Test application functionality after cleanup
- [ ] Document any data that needs to be preserved

