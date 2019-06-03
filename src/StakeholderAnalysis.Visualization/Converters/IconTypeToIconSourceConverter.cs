using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IconTypeToIconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StakeholderIconType iconType)
            {
                switch (iconType)
                {
                    case StakeholderIconType.Knowledge:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Knowledge.png";
                    case StakeholderIconType.Suit:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Pak.png";
                    case StakeholderIconType.GroupTable:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Group.png";
                    case StakeholderIconType.Dollar:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/euro.png";
                    case StakeholderIconType.Angel:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_angel.png";
                    case StakeholderIconType.Baby:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_baby.png";
                    case StakeholderIconType.Cloud:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_cloud.png";
                    case StakeholderIconType.Coffee:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_coffee.png";
                    case StakeholderIconType.Dance:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_dance.png";
                    case StakeholderIconType.Disabled:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_disabled.png";
                    case StakeholderIconType.Family:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_family.png";
                    case StakeholderIconType.Fountain:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_fountain.png";
                    case StakeholderIconType.FullMug:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_full_mug.png";
                    case StakeholderIconType.GoodListen:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_good_listen.png";
                    case StakeholderIconType.Group:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_group.png";
                    case StakeholderIconType.GroupAdd:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_group_add.png";
                    case StakeholderIconType.Group2:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_group2.png";
                    case StakeholderIconType.Group3:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_group3.png";
                    case StakeholderIconType.Group4:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_group4.png";
                    case StakeholderIconType.Group5:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Waterschap.png";
                    case StakeholderIconType.HeadPhones:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_headphones.png";
                    case StakeholderIconType.PersonAdd:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_icon-person-add.png";
                    case StakeholderIconType.PersonStalker:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_icon-person-stalker.png";
                    case StakeholderIconType.Link:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_link.png";
                    case StakeholderIconType.Link2:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_link2.png";
                    case StakeholderIconType.MorningCoffee:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_morning-coffee.png";
                    case StakeholderIconType.Person2:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_person 2.png";
                    case StakeholderIconType.Person:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_person.png";
                    case StakeholderIconType.Running:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_running.png";
                    case StakeholderIconType.Settings:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_settings.png";
                    case StakeholderIconType.Walking:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_walking.png";
                    case StakeholderIconType.Ad:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_ad.png";
                    case StakeholderIconType.MailBox:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_mailbox.png";
                    case StakeholderIconType.MailOpen:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_mail_open.png";
                    case StakeholderIconType.Mail:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_mail.png";
                    case StakeholderIconType.Yen:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_yen.png";
                    case StakeholderIconType.Money:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_money.png";
                    case StakeholderIconType.DollarBundle:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_dollar_bundle.png";
                    case StakeholderIconType.Euro:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_euro.png";
                    case StakeholderIconType.Wifi:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/iconfinder_wifi.png";
                    case StakeholderIconType.Other:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/other.png";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
