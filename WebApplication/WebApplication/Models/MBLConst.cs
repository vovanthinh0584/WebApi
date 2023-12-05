using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public static class MBLConstants
    {
        public static bool APP_MAP_DOITUONG_PHAMVI_CHOTHUOC = true;
        public static string BenhManTinh = "BenhManTinh";
        public static string Bearer = "Bearer ";
        public static string Authorization = "Authorization";
        public static string YL = "YL";
        public static int TOKEN_EXPIRATION = 5000;
        public static string exp = "exp";
        public static string KTC = "KTC";
        public static string TRUE = "1";
        public static string NOITRU = "NOITRU";
        public static string KetQuaDieuTri = "KetQuaDieuTri";
        public static string NgheNghiep = "NgheNghiep";
        public static string DUOCKEY_FORMAT = "{0}_{1}_{2}";
        public static string CapHoLy = "CapHoLy";
        public static string DIEUDUONG_CODE = "('99')";
        public static string BACSI_CODE = "('BS', 'BSCKDH', '4', '5','40','45','1','52','85')";
        public static string DuongDung = "DuongDung";
        public static string IPD24 = "IPD24";
        public static string IPD61 = "IPD61";
        public static string PhanCapChamSoc = "PhanCapChamSoc";
        public static string LOGIN_RESULT_TYPE_SUCCESS = "0";
        public static string LOGIN_RESULT_TYPE_OUT_OF_DATE = "1";
        public static string LOGIN_RESULT_TYPE_INVALID = "4";
        public static string DaKham = "Đã Khám";
        public static string ChuaKham = "Chưa Khám";
        public static string Mach = "Mạch";
        public static string HuyetAp = "Huyết Áp";
        public static string NhietDo = "Nhiệt Độ";
        public static string NhipTho = "Nhịp Thở";

        public static string DaKhamCode = "DaKham";
        public static string ChuaKhamCode = "ChuaKham";
        public static string MachCode = "Mach";
        public static string HuyetApCode = "HuyetAp";
        public static string NhietDoCode = "NhietDo";
        public static string NhipThoCode = "NhipTho";

        public static int CanhBaoMach = 40; // < 70
        public static int MaxCanhBaoMach = 131; // < 70
        public static int CanhBaoHuyetAp = 90; // >= 140
        public static int MaxCanhBaoHuyetAp = 220; // >= 140
        public static decimal CanhBaoNhietDo = 35; // >= 38.5
        public static decimal MaxCanhBaoNhietDo = 39.5m; // >= 38.5
        public static int CanhBaoNhipTho = 8; // < 16
        public static decimal MaxCanhBaoNhipTho = 25; // >= 38.5

        public const string TT_NOITRU_TOATHUOC = "TT_NOITRU_TOATHUOC";
        public const string N = "N";
        public const string SAPXEP_01 = "01";
        public const string SAPXEP_02 = "02";
        public const string SAPXEP_03 = "03";
        public const string SAPXEP_04 = "04";
        public const string SAPXEP_05 = "05";
        public const string SAPXEP_06 = "06";
        public const string SAPXEP_08 = "08";
        public const string SAPXEP_20 = "20";
        public const int SUCCESS_RESPONSE_CODE = 200;
        public const int INVALID_RESPONSE_CODE = 1000;
        public const string THANHCONG = "Thành công!";
        public const string INVALID_LOGIN = "Sai tài khoản hoặc mật khẩu!";
        public const string COM_DUNGBANGGIA_THEO_NGUONKHACHHANG = "1";
        public const string NguonBN = "NguonBN";
        public const string C0001173 = "0.5";
        public const string C0001172 = "0.5";
        public const string CHUAKETQUA = "CHUAKETQUA";
        public const string MBIL0901 = "01. Khám bệnh";
        public const string MBIL0902 = "03. Xét nghiệm";
        public const string MBIL0903 = "04. Chẩn đoán hình ảnh";
        public const string MBIL0904 = "05. Thăm dò chức năng";
        public const string MBIL0905 = "06. Thủ thuật, phẫu thuật";
        public const string MBIL0931 = "12. Tiền giường";
        public const string MBIL0911 = "08. Máu và chế phẩm máu";
        public const string MBIL0923 = "01. Khám bệnh";
        public const string MBIL0906 = "07. Dịch vụ kỹ thuật cao";
        public const string C0001277 = "Đã thực hiện";
        public const string C0001276 = "Chưa thực hiện";
        public const string C0001202 = "Ngày hết hạn: ";
        public const string C0000003 = "dd/MM/yyyy";
        public const string C0001074 = "GiaDichVu_01";
        public const string COM_KHONGIN_KHI_LAYTHONGTIN = "COM_KHONGIN_KHI_LAYTHONGTIN";
        public const string F0000144 = "Chỉ định thường quy";
        public const string F0000145 = "Chỉ định hẹn";
        public const string F0000146 = "Chỉ định bên ngoài";

        public const string IPD60_CHOPHEP_SUAYLENH_DALAPPHIEULINH = "0";
        public const string COM_CHUCNANG_DANHSACHPHAMVI = "{\"IPD61\": \"K_NOITRU,K_VC,K_DY,K_MAU,K_VTYT\",\"IPD25\":\"K_VTYT,KHO_HC\", \"OPD20\":\"\"}";
        public const string COM_SOLUONG_HOACHAT_GIOIHAN = "10";
        public const string COM_SOLUONG_THUOC_GIOIHAN = "10";
        public const string COM_SOLUONG_VATTU_GIOIHAN = "10";


        public const string C0001103 = "C0001103";
        public const string OPD_SOCAKHAMTOIDA = "OPD_SOCAKHAMTOIDA";
        public const string OPD_KHOACAKHAM_NGOAITRU = "OPD_KHOACAKHAM_NGOAITRU";
        public const string OPD_XACNHAN_KHOACAKHAM_NGOAITRU = "OPD_XACNHAN_KHOACAKHAM_NGOAITRU";
        public const string OPD_THOIHANCAPNHAT_TOAKHONGBHYT = "OPD_THOIHANCAPNHAT_TOAKHONGBHYT";
        public const string PHA_DS_MUCDOTUONGTAC_CHAPNHAN = "";
        public const string OPD_SUDUNGTOAMAU = "OPD_SUDUNGTOAMAU";
        public const string OPD_FILTER_LSDUNGTHUOC_LOAITOA = "OPD_FILTER_LSDUNGTHUOC_LOAITOA";
        public const string COM_MUCGIATHUOC_DATTIEN = "COM_MUCGIATHUOC_DATTIEN";

        public const string TT_NOITRU_KHAMBENH = "TT_NOITRU_KHAMBENH";
        public const string TT_NGOAITRU_KHAMBENH = "TT_NGOAITRU_KHAMBENH";
        public const string OPD15 = "OPD15";
        public const string REF_ID = "REF_ID";
        public const string KHAMBENH_VTYT_ID = "KHAMBENH_VTYT_ID";
        public const string CLSKETQUA_VTYT_ID = "CLSKETQUA_VTYT_ID";
        public const string TT_CLSKETQUA_VTYT = "TT_CLSKETQUA_VTYT";
        public const string TT_NGOAITRU_KHAMBENH_VTYT = "TT_NGOAITRU_KHAMBENH_VTYT";
        public const string V = "V";
        public const string COM062 = "COM062";
        public const string C0000004 = "C0000004";
        public const string EMR_SOTHANGLAYLSDUNGTHUOC = "EMR_SOTHANGLAYLSDUNGTHUOC";
        public const string BTNCHUYENVIENINFO = "BTNCHUYENVIENINFO";
        public const string KCC = "CC";

        //------------ begin -- Dung de map status code cua workflow --------
        public const string DANGDIEUTRI = "DANGDIEUTRI";
        public const string HOANTATCODE = "HOANTAT";
        public const string CHUAKETQUACODE = "CHUAKETQUA"; //Chưa kết quả
        public const string CHOXACNHANCODE = "CHOXACNHAN"; //Chờ duyệt kết quả
        public const string DANGTHUCHIENCODE = "DANGTHUCHIEN"; //Thực hiện CLS
        public const string DACHUYENTUYENCODE = "DACHUYENTUYEN";
        public const string CHODONGTIEN = "CHODONGTIEN"; //Chờ đóng tiền
        public const string HOANTAT = "HOANTAT"; //Hoàn tất dịch vụ
        public const string DUYETTOATHUOC = "DUYETTOATHUOC"; //Chờ duyệt toa thuốc BHYT
        public const string XUATTHUOC = "XUATTHUOC"; //Xuất thuốc BHYT   
        public const string DAXUATVIEN = "DAXUATVIEN"; //Tổng kết bệnh án thành công
        public const string HOANTHANH = "HOANTHANH"; //Ra viện
        public const string CHOLAYMAU = "CHOLAYMAU"; //Ra viện
        //------------ end -- Dung de map status code cua workflow --------

        public const string C0001052 = "C0001052";
        public const string C0001053 = "C0001053";
        public const string C0001054 = "C0001054";
        public const string C0001055 = "C0001055";
        public const string C0001062 = "C0001062";
        public const string C0001099 = "C0001099"; // "Khoá ca khám"
        public const string C0001100 = "C0001100"; // "Bỏ khoá ca khám"
        public const string C0001245 = "C0001245";
        public const string C0001246 = "C0001246";
        public const string C0001247 = "C0001247";

        public const string PHONGBAN = "PHONGBAN";
        public const string NOITHUCHIEN = "NOITHUCHIEN";
        public const string Duoc_ID = "Duoc_ID";
        public const string ChanDoanICD_ID = "ChanDoanICD_ID";
        public const string HuongGiaiQuyet_ID = "HuongGiaiQuyet_ID";
        public const string NgayKham = "NgayKham";
        public const string NhapKhoa_ID = "NhapKhoa_ID";
        public const string SoNgayHenTaiKham = "SoNgayHenTaiKham";
        public const string NgayHen = "NgayHen";
        public const string ChuyenDenBenhVien_ID = "ChuyenDenBenhVien_ID";
        public const string NhapKhoaNhapVien_ID = "NhapKhoaNhapVien_ID";
        public const string LyDoNhapVien_ID = "LyDoNhapVien_ID";
        public const string NghiOm_TuNgay = "NghiOm_TuNgay";
        public const string NghiOm_DenNgay = "NghiOm_DenNgay";
        public const string NGHIOM_TUNGAY = "NGHIOM_TUNGAY";
        public const string NGHIOM_DENNGAY = "NGHIOM_DENNGAY";
        public const string HUYTOA = "HUYTOA";
        public const string DAPHAT = "DAPHAT";
        public const string TT = "TT";
        public const string UT = "UT";
        public const string BV = "BV"; //Toa bệnh viện(toa BHYT)
        public const string MN = "MN"; //Toa mua ngoài (không BHYT)
        public const string MP = "MP"; //Toa miễn phí
        public const string TV = "TV"; //Toa tư vấn
        public const string SVC_KHAMBENH = "OPD002";
        public const string SVC_TOADENGHI = "OPD009";
        public const string SVC_TM_COMMON = "COM011";
        public const string SVC_BENHNHAN = "MPI001";
        public const string SVC_OPD14 = "OPD014";
        public const string COMBINE_STRING = "; ";
        public const string MINUS = "-";
        public const string CAPTOA = "CapToa";
        public const string CAPTOACHUYENTUYENDUOI = "ChuyenTuyen";
        public const string CAPTOANGHIOM = "NghiOm";
        public const string CAPTOAHENTAIKHAM = "HenTaiKham";
        public const string HENLAYKETQUACLS = "KetQuaCLS";
        public const string DIEUTRINGOAITRU = "DTNgoaiTru";
        public const string CHOTHUCHIENCLS = "ChiDinhCLS";
        public const string KHONGTOA = "KT";
        public const string CHUYENVIEN = "ChuyenVien";
        public const string NHAPVIEN = "NhapVien";
        public const string CAPCUU = "CapCuu";
        public const string DIEUTRINGOAITRU_DALIEU = "DTNgoaiTruDalieu";
        public const string CHUAXUAT = "CHUAXUAT";
        public const string SOTHUTUTOA_NGOAITRU = "SOTHUTUTOA_NGOAITRU";
        public const string DAO_LISTKEY_EXAM = "DAO00026";
        public const string STATUS = "STATUS";
        public const string SOLUONG = "SOLUONG";
        public const string COM002 = "COM002";
        public const string COM004 = "COM004";
        public const string COM003 = "COM003";
        public const string COM052 = "COM052";
        public const string OPD013 = "OPD013";
        public const string REG001 = "REG001";
        public const string IPD012 = "IPD012";
        public const string DRI001 = "DRI001";
        public const string KHONGOAITRU_MAKHO = "BHYT";
        public const string KHOBENHVIEN_MAKHO = "BV";
        public const int DOITUONG_BHYT_ID = -1;
        public const string C0001050 = "C0001050";
        public const string C0001051 = "C0001051";
        public const string SPACE = " ";
        public const string MESSAGE = "MESSAGE";
        public const string TT_NGOAITRU_TOATHUOC = "TT_NGOAITRU_TOATHUOC";
        public const string TT_DVYEUCAU = "TT_DVYEUCAU";
        public const string VMOPD13 = "VMOPD13";
        public const string VMOPD43 = "VMOPD43";
        //public const string Code_MaKho_Chan = "MaKho_Chan";
        public const string Code_MaKho_Chan = "PHA_MAKHO_CHAN";
        public const string COMMA = ",";
        public const char CHAR_COMBINE_STRING = ';';
        public const string T = "T";
        public const string H = "H";
        public const string OPD_FILTERVTYT = "OPD_FILTERVTYT";
        public const string OPD_SONGAYDUNGTHUOCTOIDA = "OPD_SONGAYDUNGTHUOCTOIDA";
        public const string NGUONLAYTHUOC_TT = "D";
        public const string _ddMMyy_hhmmss = "_ddMMyy_hhmmss";
        public const string CheckBox = "CheckBox";
        public const string RadioButton = "RadioButton";
        public const string OpenControl = "{[{";
        public const string CloseControl = "}]}";
    }
}
