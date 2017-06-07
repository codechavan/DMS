----Module Name			-DB Level		-Middle Layer		-API			-WEB PAGE		-Testing

--1. System creation
--	a. Add(Registration)- DONE			- DONE			-DONE
--	b. Edit				- DONE			- DONE
--	c. List				- DONE			- DONE
--	d. SystemCode(AphaNum)- PENDING

--2. Role Creation
--	a. Add				- DONE			- DONE
--	b. Edit				- DONE			- DONE
--	c. List				- DONE			- DONE

--3. User creation	
--	a. Add				- DONE			- DONE
--	b. Edit				- DONE			- DONE
--	c. Change Password	- DONE			- DONE
--	d. Unlock user		- DONE			- DONE
--	e. Login			- DONE			- DONE			-DONE
--	f. List				- DONE			- DONE
--	g. Generate Token	- DONE			- DONE
--	h. AuthenticateToken- DONE			- DONE

--4. System Parameter / System Parameter Values
--	a. Update			- DONE			- DONE
--	b. Master List		- DONE			- DONE
--	c. Value List		- DONE			- DONE
--	d. Function			- DONE			- DONE

--5. System Admin
--	a. Add/Edit			- DONE			- DONE
--	b. ChangePassword	- DONE			- DONE
--	c. User List		- DONE			- DONE
--	d. Login			- DONE			- DONE
--	e. Generate Token	- DONE			- DONE
--	f. AuthenticateToken- DONE			- DONE

--6. Configuration
--	a. Update			- DONE			- DONE
--	b. List				- DONE			- DONE
--	c. Function			- DONE			- DONE

--7. Document Properties Name Configuration
--	a. Add/Update		- DONE			- DONE
--	b. List				- DONE			- DONE

--8. Document Folder(Pending - Add logic for check rights)
--	a. Add				- DONE			- DONE
--	b. Edit				- DONE			- DONE
--	c. List				- DONE			- DONE
--	d. Delete			- PENDING
--	e. DirectoryPath	- PENDING(Function)

--9. Document Files(Pending - Add logic for check rights)
--	a. Add				- DONE			- DONE
--	b. Edit				- DONE			- DONE
--	c. List				- DONE			- DONE
--	d. History			- DONE			- DONE
--	d. Delete			- PENDING
--	d. View				- PENDING

--10. Document Properites(Pending - Add logic for check rights)
--	a. Add				- DONE			- DONE
--	b. Edit				- DONE			- DONE
--	c. GET				- DONE			- DONE

--11. UserRole Mapping to Folder & Files
--	a. Assign Rights to file/folder		- DONE	- DONE
--	c. Remove rights from file/folder	- DONE	- DONE
--	e. Has access						- DONE	- DONE

--12. Miscellinous [For Authentication]  -- Not required currently, low priority
--	a. Store Session Token in DB [SessionID, Token, UserID, Token, CreatedByIP, CreatedOn, LastAccessOn, IsActive, LogoutOn, LogoutRemark]
--	b. Parameter for session time out period
--	c. Parameter for Single Signon
--	d. Add more key in Session token parameter [SessionID, ReferalIP]
--	f. Create Configuration for Allow IP for API
--	e. Replace Order By And search filder by hashtable


--13. API for upload integration
--	a. Mapping screen - UploadCode to our dms folder(UploadCode will be unique for there systemid) with description
--	b. Upload API, will consist of DocumentDetails along with UploadCode, so as per UploadCode file will get uploaded to that folder in our system
