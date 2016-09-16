# QIQO.Business.Client.Solution
### WPF, MVVM, XAML, C#, Prism

QIQO Business Application Windows client solution. This depends on the [QIQO.Business.Services.Solution][1] project for WCF calls performed via client side proxies (ChannelFactory). This solution utilizes the [Prism][2] framework.

This include 2 user interfaces: 
- [QIQO.Business.Client.UI][3] is a WPF UI that includes a Ribbon, and is a more classic Windows look and feel. 
- [QIQO.Business.Client.UIX][4] is a WPF UI that has a more modern look and feel, and uses some animation when transitioning from  one view to another. 

- [QIQO.Business.Client.Contracts][5] - interfaces specific to this solution
- [QIQO.Business.Client.Core.UI][6] - shared UI related code
- [QIQO.Business.Client.Core][7] - shared code
- [QIQO.Business.Client.Entities][8] - Business entities (POCOs)
- [QIQO.Business.Client.Proxies][9] - WCF proxies
- [QIQO.Business.Client.Tests][10] - Unit tests
- [QIQO.Business.Client.UI][3] - Classic Windows UI (Prism)
- [QIQO.Business.Client.UIX][4] - More modern Windows UI (Prism)
- [QIQO.Business.Client.Wrappers][11] - Entity wrappers (thanks to [Thomas Claudius Huber][20])
- [QIQO.Business.Module.Account][12] - Account module (Prism)
- [QIQO.Business.Module.Company][13] - Company module (Prism)
- [QIQO.Business.Module.Dashboard][14] - Dashboard module (Prism)
- [QIQO.Business.Module.General][15] - General (shared) module (Prism)
- [QIQO.Business.Module.Invoice][16] - Invoice module (Prism)
- [QIQO.Business.Module.Orders][17] - Orders module (Prism)
- [QIQO.Business.Module.Product][18] - Product module (Prism)
- [QIQO.Custom.Controls][19] - Custom controls library


[1]: https://github.com/rdrrichards/QIQO.Business.Services.Solution
[2]: https://github.com/PrismLibrary/Prism
[3]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.UI
[4]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.UIX
[5]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Contracts
[6]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Core.UI
[7]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Core
[8]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Entities
[9]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Proxies
[10]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Tests
[11]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Client.Wrappers
[12]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.Account
[13]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.Company
[14]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.Dashboard
[15]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.General
[16]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.Invoice
[17]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.Orders
[18]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Business.Module.Product
[19]: https://github.com/rdrrichards/QIQO.Business.Client.Solution/tree/master/QIQO.Custom.Controls
[20]: http://www.thomasclaudiushuber.com/
