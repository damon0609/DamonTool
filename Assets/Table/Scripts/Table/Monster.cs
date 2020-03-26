using System;
namespace Damon.Table
{
    public class Monster : BaseTable
    {
        public Monster(string name) : base(name)
        { }

        protected override void Clear()
        {
            throw new NotImplementedException();
        }

        protected override void InitData()
        {
        }

        protected override void InitView()
        {
        }

        public override void ItemChange()
        {
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
