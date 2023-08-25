using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YiYi.Entity.AttributeManager;

namespace YiYi.Entity.DomainModels.Order
{
    [Table("OrderDet")]
    [EntityTab(TableCnName = "订单明细")]
    public class OrderDet : BaseEntity
    {
        [Key]
        [Display(Name = "OrderDet_Id")]
        [MaxLength(36)]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid OrderDet_Id { get; set; }

        /// <summary>
        ///订单Id
        /// </summary>
        [Display(Name = "订单Id")]
        [MaxLength(36)]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid Order_Id { get; set; }

        /// <summary>
        ///商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string ProductName { get; set; }

        /// <summary>
        ///重量
        /// </summary>
        [Display(Name = "重量")]
        [DisplayFormat(DataFormatString = "11,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? Weight { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }
    }
}
