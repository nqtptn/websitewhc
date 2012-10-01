using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using CommonWebSite;
using ModelWebSite.ModelDB;

namespace BusinessWebSite
{
    public class HeThongBL
    {
        private static EntityBL _entityBL = new EntityBL();
        public static EntityBL entityBL
        {
            get
            {
                _entityBL = new EntityBL();
                _entityBL.CreateDataContext += new EntityBL.CreateDataContextHandler(entityBL_CreateDataContext);
                return _entityBL;
            }
        }
        private static DataContext entityBL_CreateDataContext()
        {
            return new ModelWebSite.ModelDB.ModelSysDataContext();
        }

        #region NguoiDung
        public static List<Employee> GetListNguoiDung()
        {
            try
            {
                return entityBL.GetEntityList<Employee>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool CheckLogin(string username, string password)
        {
            try
            {
                Employee nguoiDung = entityBL.GetEntity<Employee>().Where(item => item.Username == username && item.Password == password).SingleOrDefault();
                if (nguoiDung != null)
                {
                    HttpContext.Current.Session["TenHienThi"] = nguoiDung.FullName;
                    HttpContext.Current.Session["userName"] = nguoiDung.Username;
                    HttpContext.Current.Session["IDNhomND"] = nguoiDung.IDuserlevels;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteNguoiDung(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<Employee>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateNguoiDung(Employee entity)
        {
            try
            {
                return entityBL.UpdateEntity<Employee>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddNguoiDung(Employee entity)
        {
            try
            {
                return entityBL.InsertEntity<Employee>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Employee GetNguoidungByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<Employee>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion NguoiDung
        #region Funtion
        public static List<Sps_PhanQuyenResult> GetListFuntionGroupUser(string IDUserLevel)
        {
            ModelSysDataContext model = new ModelSysDataContext();
            return model.Sps_PhanQuyen(IDUserLevel).ToList();
        }
        public static List<Funtion> GetListQuyenByNhom(string IDNhomND)
        {
            try
            {
                List<string> listPQNhom = entityBL.GetEntityList<UserLevelPermission>().Where(item => item.IDuserlevels == IDNhomND && item.Read == true).Select(item=>item.IDfuntions).ToList();
                return entityBL.GetEntityList<Funtion>().Where(item => listPQNhom.Contains(item.ID) && item.use == true).ToList();
                //VwHTPQNhomCollection colChucnang = new VwHTPQNhomCollection()
                //        .Where(VwHTPQNhom.Columns.IDNhomND, Session["IDNhomND"])
                //        .Where(VwHTPQNhom.Columns.Xem, true)
                //        .Where(VwHTPQNhom.Columns.SuDung, true)
                //        .OrderByAsc(VwHTPQNhom.Columns.MaThuTu)
                //        .Load();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<Funtion> GetListChucNang()
        {
            try
            {
                return entityBL.GetEntityList<Funtion>().OrderBy(item => item.sequence).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Funtion GetChucnangByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<Funtion>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteChucNang(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<Funtion>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateChucNang(Funtion entity)
        {
            try
            {
                return entityBL.UpdateEntity<Funtion>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddChucNang(Funtion entity)
        {
            try
            {
                return entityBL.InsertEntity<Funtion>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Funtion
        #region HT_NhomND
        public static List<UserLevel> GetListNhomND()
        {
            try
            {
                return entityBL.GetEntityList<UserLevel>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static UserLevel GetNhomNDByID(String ID)
        {
            try
            {
                return entityBL.GetEntityByID<UserLevel>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteNhomND(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<UserLevel>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateNhomND(UserLevel entity)
        {
            try
            {
                return entityBL.UpdateEntity<UserLevel>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddNhomND(UserLevel entity)
        {
            try
            {
                return entityBL.InsertEntity<UserLevel>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
       
        #region Userlevel
        public static List<UserLevel> GetListUserlevel()
        {
            try
            {
                return entityBL.GetEntityList<UserLevel>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static UserLevel GetUserlevelByID(String ID)
        {
            try
            {
                return entityBL.GetEntityByID<UserLevel>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteUserlevel(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<UserLevel>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateUserlevel(UserLevel entity)
        {
            try
            {
                return entityBL.UpdateEntity<UserLevel>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddUserlevel(UserLevel entity)
        {
            try
            {
                return entityBL.InsertEntity<UserLevel>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        
        #region UserLevelPermission
        public static List<UserLevelPermission> GetListUserLevelPermission()
        {
            try
            {
                return entityBL.GetEntityList<UserLevelPermission>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static UserLevelPermission GetUserLevelPermissionByID(String ID)
        {
            try
            {
                return entityBL.GetEntityByID<UserLevelPermission>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteUserLevelPermissionByIDNhomND(string IdNhomND)
        {
            try
            {
                ModelSysDataContext data = new ModelSysDataContext();
                var listPhanQuyen = data.UserLevelPermissions.Where(item => item.IDuserlevels == IdNhomND).ToList();
                if (listPhanQuyen.Count > 0)
                {
                    try
                    {
                        data.UserLevelPermissions.DeleteAllOnSubmit(listPhanQuyen);
                        data.SubmitChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteUserlevelpermission(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<UserLevelPermission>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateUserlevelpermission(UserLevelPermission entity)
        {
            try
            {
                return entityBL.UpdateEntity<UserLevelPermission>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddUserlevelpermission(UserLevelPermission entity)
        {
            try
            {
                return entityBL.InsertEntity<UserLevelPermission>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddListUserlevelpermission(List<UserLevelPermission> entity)
        {
            try
            {
                return entityBL.InsertEntityList<UserLevelPermission>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Category
        public static List<Category> GetListCategory()
        {
            try
            {
                return entityBL.GetEntityList<Category>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteCategory(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<Category>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateCategory(Category entity)
        {
            try
            {
                return entityBL.UpdateEntity<Category>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddCategory(Category entity)
        {
            try
            {
                return entityBL.InsertEntity<Category>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Category GetCategoryByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<Category>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Category
        
        #region Product
        public static List<Product> GetListProduct()
        {
            try
            {
                return entityBL.GetEntityList<Product>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteProduct(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<Product>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateProduct(Product entity)
        {
            try
            {
                return entityBL.UpdateEntity<Product>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddProduct(Product entity)
        {
            try
            {
                return entityBL.InsertEntity<Product>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Product GetProductByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<Product>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Product

        #region Info
        public static List<Info> GetListInfo()
        {
            try
            {
                return entityBL.GetEntityList<Info>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteInfo(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<Info>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateInfo(Info entity)
        {
            try
            {
                return entityBL.UpdateEntity<Info>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddInfo(Info entity)
        {
            try
            {
                return entityBL.InsertEntity<Info>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Info GetInfoByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<Info>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Product

        #region Comment
        public static List<Comment> GetListComment()
        {
            try
            {
                return entityBL.GetEntityList<Comment>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteComment(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<Comment>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateComment(Comment entity)
        {
            try
            {
                return entityBL.UpdateEntity<Comment>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddComment(Comment entity)
        {
            try
            {
                return entityBL.InsertEntity<Comment>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Comment GetCommentByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<Comment>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Comment

        #region ImageLibrary
        public static List<ImageLibrary> GetListImageLibrary()
        {
            try
            {
                return entityBL.GetEntityList<ImageLibrary>().OrderBy(item => item.ID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteImageLibrary(string ID)
        {
            try
            {
                //return true;
                return entityBL.DeleteEntityByID<ImageLibrary>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool UpdateImageLibrary(ImageLibrary entity)
        {
            try
            {
                return entityBL.UpdateEntity<ImageLibrary>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddImageLibrary(ImageLibrary entity)
        {
            try
            {
                return entityBL.InsertEntity<ImageLibrary>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static ImageLibrary GetImageLibraryByID(string ID)
        {
            try
            {
                return entityBL.GetEntityByID<ImageLibrary>(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion ImageLibrary
    }
}
