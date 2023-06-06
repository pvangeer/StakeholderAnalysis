using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IconTypeToIconSourceConverter : IValueConverter
    {
        private readonly string stakeholderTypeIconPrefix =
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/StakeholderTypeIcons/";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StakeholderIconType iconType)
                switch (iconType)
                {
                    case StakeholderIconType.Knowledge:
                        return
                            stakeholderTypeIconPrefix + "Knowledge.png";
                    case StakeholderIconType.Suit:
                        return stakeholderTypeIconPrefix + "Pak.png";
                    case StakeholderIconType.GroupTable:
                        return stakeholderTypeIconPrefix + "Group.png";
                    case StakeholderIconType.Dollar:
                        return stakeholderTypeIconPrefix + "euro.png";
                    case StakeholderIconType.Angel:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_angel.png";
                    case StakeholderIconType.Baby:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_baby.png";
                    case StakeholderIconType.Cloud:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_cloud.png";
                    case StakeholderIconType.Coffee:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_coffee.png";
                    case StakeholderIconType.Dance:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_dance.png";
                    case StakeholderIconType.Disabled:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_disabled.png";
                    case StakeholderIconType.Family:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_family.png";
                    case StakeholderIconType.Fountain:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_fountain.png";
                    case StakeholderIconType.FullMug:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_full_mug.png";
                    case StakeholderIconType.GoodListen:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_good_listen.png";
                    case StakeholderIconType.Group:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_group.png";
                    case StakeholderIconType.GroupAdd:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_group_add.png";
                    case StakeholderIconType.Group2:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_group2.png";
                    case StakeholderIconType.Group3:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_group3.png";
                    case StakeholderIconType.Group4:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_group4.png";
                    case StakeholderIconType.Group5:
                        return
                            stakeholderTypeIconPrefix + "Waterschap.png";
                    case StakeholderIconType.HeadPhones:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_headphones.png";
                    case StakeholderIconType.PersonAdd:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_icon-person-add.png";
                    case StakeholderIconType.PersonStalker:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_icon-person-stalker.png";
                    case StakeholderIconType.Link:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_link.png";
                    case StakeholderIconType.Link2:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_link2.png";
                    case StakeholderIconType.MorningCoffee:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_morning-coffee.png";
                    case StakeholderIconType.Person2:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_person 2.png";
                    case StakeholderIconType.Person:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_person.png";
                    case StakeholderIconType.Running:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_running.png";
                    case StakeholderIconType.Settings:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_settings.png";
                    case StakeholderIconType.Walking:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_walking.png";
                    case StakeholderIconType.Ad:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_ad.png";
                    case StakeholderIconType.MailBox:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_mailbox.png";
                    case StakeholderIconType.MailOpen:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_mail_open.png";
                    case StakeholderIconType.Mail:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_mail.png";
                    case StakeholderIconType.Yen:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_yen.png";
                    case StakeholderIconType.Money:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_money.png";
                    case StakeholderIconType.DollarBundle:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_dollar_bundle.png";
                    case StakeholderIconType.Euro:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_euro.png";
                    case StakeholderIconType.Wifi:
                        return
                            stakeholderTypeIconPrefix + "iconfinder_wifi.png";
                    case StakeholderIconType.Other:
                        return stakeholderTypeIconPrefix + "other.png";
                }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}