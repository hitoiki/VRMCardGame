using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public static class StaticSkill
{
    public static Skill costPay = new Skill("withCost", (x) =>
        {
            return Observable.Defer<Unit>(() =>
               {
                   x.CostPay();
                   return Observable.Empty<Unit>();
               });
        },
         x => { return true; });

    public static Skill turnEnd = new Skill("turnEnd", (x) =>
            {
                return Observable.Defer<Unit>(() =>
                   {
                       x.CostPay();
                       return Observable.Empty<Unit>();
                   });
            },
             x => { return true; });
}
