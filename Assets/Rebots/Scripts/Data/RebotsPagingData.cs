using System;

namespace Assets.Rebots
{
    [Serializable]
    public class RebotsPagingData<T>
    {
        public T Data { get; private set; }
        public int Page { get; private set; }
        public int TotalPage { get; private set; }
        public int SelectedPage { get; private set; }
        public bool IsClickAction { get; private set; }

        public RebotsPagingData(T data)
        {
            this.Data = data;
            this.Page = 1;
            this.IsClickAction = true;
        }

        public RebotsPagingData(T data, int totalPage, int selectedPage)
        {
            this.Data = data;
            this.Page = 1;
            this.TotalPage = totalPage;
            this.SelectedPage = selectedPage;
            this.IsClickAction = true;
        }

        public RebotsPagingData<T> SetPage(int page)
        {
            return new RebotsPagingData<T>(this.Data) { Page = page, TotalPage = this.TotalPage, SelectedPage = this.SelectedPage, IsClickAction = (page != SelectedPage) };
        }

        public RebotsPagingData<T> SetStartPage()
        {
            return new RebotsPagingData<T>(this.Data) { Page = 1, TotalPage = this.TotalPage, SelectedPage = this.SelectedPage, IsClickAction = (SelectedPage != 1) };
        }

        public RebotsPagingData<T> SetPreviousPage()
        {
            return new RebotsPagingData<T>(this.Data) { Page = (this.SelectedPage - 1), TotalPage = this.TotalPage, SelectedPage = this.SelectedPage, IsClickAction = (SelectedPage != 1) };
        }

        public RebotsPagingData<T> SetNextPage()
        {
            return new RebotsPagingData<T>(this.Data) { Page = (this.SelectedPage + 1), TotalPage = this.TotalPage, SelectedPage = this.SelectedPage, IsClickAction = (SelectedPage != TotalPage) };
        }

        public RebotsPagingData<T> SetEndPage()
        {
            return new RebotsPagingData<T>(this.Data) { Page = this.TotalPage, TotalPage = this.TotalPage, SelectedPage = this.SelectedPage, IsClickAction = (SelectedPage != TotalPage) };
        }
    }
}
