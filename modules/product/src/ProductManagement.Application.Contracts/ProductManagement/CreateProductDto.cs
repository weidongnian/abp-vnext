using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement
{
    public class CreateProductDto: IValidatableObject
    {
        [Required]
        [StringLength(ProductConsts.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(ProductConsts.MaxImageNameLength)]
        public string ImageName { get; set; }

        public float Price { get; set; }

        public int StockCount { get; set; }

        /// <summary>
        /// Dto 验证拓展
        /// 虽然可以在 Validate 方法中解析服务实现任何可能性,
        /// 但在DTO中实现领域验证逻辑不是一个很好的做法. 
        /// 应保持简单的DTO,他们的目的是传输数据(DTO:数据传输对象).
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0)
            {
                yield return new ValidationResult("价格不能小于0", new[] { "Price" });
            }
        }
    }
}