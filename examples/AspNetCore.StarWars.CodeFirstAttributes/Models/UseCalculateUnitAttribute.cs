using HotChocolate.Types;

namespace StarWars.Models
{
    public class UseCalculateUnitAttribute : DescriptorAttribute
    {
        protected override void TryConfigure(IDescriptor descriptor)
        {
            if (descriptor is IInterfaceFieldDescriptor interfaceField)
            {
                interfaceField
                    .Argument("unit", a => a.Type<EnumType<Unit>>());
            }
            else if (descriptor is IObjectFieldDescriptor objectField)
            {
                objectField
                    .Argument("unit", a => a.Type<EnumType<Unit>>())
                    .Use(next => async ctx =>
                    {
                        await next(ctx);

                        Unit unit = ctx.Argument<Unit>("unit");

                        if (unit == Unit.Foot && ctx.Result is double value)
                        {
                            ctx.Result = value * 3.28084d;
                        }
                    });
            }
        }
    }
}
