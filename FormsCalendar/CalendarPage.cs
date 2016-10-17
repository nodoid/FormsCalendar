using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FormsCalendar
{
    public class CalendarPage : ContentPage
    {
        int year, month;

        public CalendarPage()
        {
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            CreateUI(month, year);
        }

        DateTime FirstDayOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        void CreateUI(int mo, int yr)
        {
            var dtn = new DateTime(year, month, DateTime.Now.Day);

            var months = new List<string>
            {
                 "January", "February", "March", "April",
                 "May", "June", "July", "August",
                 "September", "October", "November", "December"
            };

            var lblDate = new Label
            {
                Text = string.Format("{0} {1}", months[month - 1], year.ToString()),
                BackgroundColor = Color.Red,
                WidthRequest = App.ScreenSize.Width * .8,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            var currentDay = dtn.Day;

            var btnBack = new Button
            {
                Text = "<",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                WidthRequest = App.ScreenSize.Width * .1,
            };

            var btnNext = new Button
            {
                Text = ">",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                WidthRequest = App.ScreenSize.Width * .1
            };

            var stackTop = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { btnBack, lblDate, btnNext }
            };

            var width = (App.ScreenSize.Width * .6) / 7;
            var height = (App.ScreenSize.Height * .4) / 7;

            var grid = new Grid
            {
                WidthRequest = App.ScreenSize.Width * .7 + 8,
                HeightRequest = App.ScreenSize.Height * .5,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(8, 0),
                RowDefinitions =
                {
                    new RowDefinition { Height = height },
                    new RowDefinition { Height = height },
                    new RowDefinition { Height = height },
                    new RowDefinition { Height = height },
                    new RowDefinition { Height = height },
                    new RowDefinition { Height = height },
                    new RowDefinition { Height = height }
                },
                ColumnSpacing = 4,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = width },
                    new ColumnDefinition { Width = width },
                    new ColumnDefinition { Width = width },
                    new ColumnDefinition { Width = width },
                    new ColumnDefinition { Width = width },
                    new ColumnDefinition { Width = width },
                    new ColumnDefinition { Width = width },
                }
            };

            var dayLabels = CreateDayLabels();
            var dateLabels = CreateDateLabels(dtn);

            int left = 0, top = 0;
            foreach (var dl in dayLabels)
                grid.Children.Add(dl, left++, top);

            left = (int)FirstDayOfMonth(dtn).DayOfWeek;
            top++;

            if (left < 0)
                left = 0;

            foreach (var dl in dateLabels)
            {
                grid.Children.Add(dl, left, top);

                left++;
                if (left == 7)
                {
                    left = 0;
                    top++;
                }
            }

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HeightRequest = App.ScreenSize.Height * .8,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    new StackLayout
                    {
                        BackgroundColor = Color.Gray,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = App.ScreenSize.Width * .70 + 8,
                        Children = {
                            stackTop,
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Children = {grid}
                    }
                        }
                    }
                }
            };

            btnBack.Clicked += delegate
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year -= 1;
                }

                CreateUI(month, year);
            };

            btnNext.Clicked += delegate
            {
                month++;
                if (month == 13)
                {
                    month = 1;
                    year += 1;
                }

                CreateUI(month, year);
            };
        }

        ObservableCollection<Label> CreateDayLabels()
        {
            return new ObservableCollection<Label>
            {
                new Label {Text = "Sunday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
                new Label {Text = "Monday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
                new Label {Text = "Tuesday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
                new Label {Text = "Wednesday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
                new Label {Text = "Thursday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
                new Label {Text = "Friday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
                new Label {Text = "Saturday", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center},
            };
        }

        ObservableCollection<Label> CreateDateLabels(DateTime today)
        {
            var labelList = new ObservableCollection<Label>();
            var color = Color.White;

            for (var n = 0; n < DateTime.DaysInMonth(today.Year, today.Month); ++n)
            {
                if (today.Month == DateTime.Now.Month && today.Year == DateTime.Now.Year)
                {
                    if (n + 1 < today.Day)
                        color = Color.Gray;
                    else
                        color = Color.White;
                }
                else
                {
                    if (today.Month < DateTime.Now.Month)
                        color = Color.Gray;
                    else
                        color = Color.White;
                }

                labelList.Add
                (
                    new Label
                    {
                        BackgroundColor = color,
                        Text = (n + 1).ToString(),
                        StyleId = (n + 1).ToString(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    }
                );
            }
            return labelList;
        }
    }
}

