

function CustomersFilterWidget() {
    
    this.getAssociatedTypes = function () {
        return ["CustomCompanyNameFilterWidget"];
    };
    
    this.onShow = function () {
        
    };

    this.showClearFilterButton = function () {
        return true;
    };
  
    this.onRender = function (container, lang, typeName, values, cb, data) {

        this.cb = cb;
        this.container = container;
        this.lang = lang;

        this.value = values.length > 0 ? values[0] : { filterType: 1, filterValue: "" };

        this.renderWidget(); 
        this.loadCustomers(); 
        this.registerEvents(); 
    };
    this.renderWidget = function () {
        var html = '<p style="font-weight:bold;">Filtrelenicek Dereceyi Seçiniz :</p>\
                    <div id="cutomerlistDivID" style="width:220px;height:200px;overflow-y:auto;" class="grid-filter-type customerslist form-control"></div><br />\
                    <div class="grid-filter-buttons">\
                        <button type="button" class="btn btn-primary grid-apply" >' + this.lang.applyFilterButtonText + '</button>\
                    </div>';
        this.container.append(html);
    };
    
    this.loadCustomers = function () {
        var $this = this;
        $.post("/OyunProfilleri/LolDereceGetirAjax", function (data) {
            $this.fillCustomers(data);
        });
    };
    
    this.fillCustomers = function (items) {
        var customerList = this.container.find(".customerslist");
        for (var i = 0; i < items.length; i++) {
            customerList.append('<input type = "checkbox" style="margin: 4px 8px 0;"' + (this.value.filterValue.indexOf(items[i]) > -1 ? 'checked="checked"' : '') + ' value="' + items[i] + '">' + items[i] + '</input><br />');
        }
    };
   
    this.registerEvents = function () {
        
        var customerList = this.container.find(".customerslist");
        
        var applyBtn = this.container.find(".grid-apply");
        
        var $context = this;
        
        applyBtn.click(function () {
            
            var type = 1;
            
            var value = "";
            
            $('.customerslist input:checked').each(function (index, element) {
                if (index == 0) {
                    value += element.value;
                }
                else {
                    value += "|" + element.value;
                }
            });

            var filterValues = [{ filterType: type, filterValue: value }];
            $context.cb(filterValues);
        });
        
        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        });
    };

}

function CustomersFilterWidgetLolRoller() {

    this.getAssociatedTypes = function () {
        return ["LolRoller"];
    };

    this.onShow = function () {

    };

    this.showClearFilterButton = function () {
        return true;
    };

    this.onRender = function (container, lang, typeName, values, cb, data) {

        this.cb = cb;
        this.container = container;
        this.lang = lang;

        this.value = values.length > 0 ? values[0] : { filterType: 1, filterValue: "" };

        this.renderWidget();
        this.loadCustomers();
        this.registerEvents();
    };
    this.renderWidget = function () {
        var html = '<p style="font-weight:bold;">Filtrelenicek Rolleri Seçiniz :</p>\
                    <div id="cutomerlistDivID" style="width:220px;height:135px;overflow-y:auto;" class="grid-filter-type customerslist form-control"></div><br />\
                    <div class="grid-filter-buttons">\
                        <button type="button" class="btn btn-primary grid-apply" >' + this.lang.applyFilterButtonText + '</button>\
                    </div>';
        this.container.append(html);
    };

    this.loadCustomers = function () {
        var $this = this;
        $.post("/OyunProfilleri/LolRolGetirAjax", function (data) {
            $this.fillCustomers(data);
        });
    };

    this.fillCustomers = function (items) {
        var customerList = this.container.find(".customerslist");
        for (var i = 0; i < items.length; i++) {
            customerList.append('<input type = "checkbox" style="margin: 4px 8px 0;"' + (this.value.filterValue.indexOf(items[i]) > -1 ? 'checked="checked"' : '') + ' value="' + items[i] + '">' + items[i] + '</input><br />');
        }
    };

    this.registerEvents = function () {

        var customerList = this.container.find(".customerslist");

        var applyBtn = this.container.find(".grid-apply");

        var $context = this;

        applyBtn.click(function () {

            var type = 1;

            var value = "";

            $('.customerslist input:checked').each(function (index, element) {
                if (index == 0) {
                    value += element.value;
                }
                else {
                    value += "|" + element.value;
                }
            });

            var filterValues = [{ filterType: type, filterValue: value }];
            $context.cb(filterValues);
        });

        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        });
    };

}

function CustomersFilterWidgetCsDereceler() {

    this.getAssociatedTypes = function () {
        return ["csdereceler"];
    };

    this.onShow = function () {

    };

    this.showClearFilterButton = function () {
        return true;
    };

    this.onRender = function (container, lang, typeName, values, cb, data) {

        this.cb = cb;
        this.container = container;
        this.lang = lang;

        this.value = values.length > 0 ? values[0] : { filterType: 1, filterValue: "" };

        this.renderWidget();
        this.loadCustomers();
        this.registerEvents();
    };
    this.renderWidget = function () {
        var html = '<p style="font-weight:bold;">Filtrelenicek Dereceyi Seçiniz :</p>\
                    <div id="cutomerlistDivID" style="width:220px;height:200px;overflow-y:auto;" class="grid-filter-type customerslist form-control"></div><br />\
                    <div class="grid-filter-buttons">\
                        <button type="button" class="btn btn-primary grid-apply" >' + this.lang.applyFilterButtonText + '</button>\
                    </div>';
        this.container.append(html);
    };

    this.loadCustomers = function () {
        var $this = this;
        $.post("/OyunProfilleri/CsDereceGetirAjax", function (data) {
            $this.fillCustomers(data);
        });
    };

    this.fillCustomers = function (items) {
        var customerList = this.container.find(".customerslist");
        for (var i = 0; i < items.length; i++) {
            customerList.append('<input type = "checkbox" style="margin: 4px 8px 0;"' + (this.value.filterValue.indexOf(items[i]) > -1 ? 'checked="checked"' : '') + ' value="' + items[i] + '">' + items[i] + '</input><br />');
        }
    };

    this.registerEvents = function () {

        var customerList = this.container.find(".customerslist");

        var applyBtn = this.container.find(".grid-apply");

        var $context = this;

        applyBtn.click(function () {

            var type = 1;

            var value = "";

            $('.customerslist input:checked').each(function (index, element) {
                if (index == 0) {
                    value += element.value;
                }
                else {
                    value += "|" + element.value;
                }
            });

            var filterValues = [{ filterType: type, filterValue: value }];
            $context.cb(filterValues);
        });

        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        });
    };

}

function CustomersFilterWidgetValorantDereceler() {

    this.getAssociatedTypes = function () {
        return ["valdereceler"];
    };

    this.onShow = function () {

    };

    this.showClearFilterButton = function () {
        return true;
    };

    this.onRender = function (container, lang, typeName, values, cb, data) {

        this.cb = cb;
        this.container = container;
        this.lang = lang;

        this.value = values.length > 0 ? values[0] : { filterType: 1, filterValue: "" };

        this.renderWidget();
        this.loadCustomers();
        this.registerEvents();
    };
    this.renderWidget = function () {
        var html = '<p style="font-weight:bold;">Filtrelenicek Dereceyi Seçiniz :</p>\
                    <div id="cutomerlistDivID" style="width:220px;height:200px;overflow-y:auto;" class="grid-filter-type customerslist form-control"></div><br />\
                    <div class="grid-filter-buttons">\
                        <button type="button" class="btn btn-primary grid-apply" >' + this.lang.applyFilterButtonText + '</button>\
                    </div>';
        this.container.append(html);
    };

    this.loadCustomers = function () {
        var $this = this;
        $.post("/OyunProfilleri/ValorantDereceGetirAjax", function (data) {
            $this.fillCustomers(data);
        });
    };

    this.fillCustomers = function (items) {
        var customerList = this.container.find(".customerslist");
        for (var i = 0; i < items.length; i++) {
            customerList.append('<input type = "checkbox" style="margin: 4px 8px 0;"' + (this.value.filterValue.indexOf(items[i]) > -1 ? 'checked="checked"' : '') + ' value="' + items[i] + '">' + items[i] + '</input><br />');
        }
    };

    this.registerEvents = function () {

        var customerList = this.container.find(".customerslist");

        var applyBtn = this.container.find(".grid-apply");

        var $context = this;

        applyBtn.click(function () {

            var type = 1;

            var value = "";

            $('.customerslist input:checked').each(function (index, element) {
                if (index == 0) {
                    value += element.value;
                }
                else {
                    value += "|" + element.value;
                }
            });

            var filterValues = [{ filterType: type, filterValue: value }];
            $context.cb(filterValues);
        });

        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        });
    };

}

function CustomersFilterWidgetValorantRoller() {

    this.getAssociatedTypes = function () {
        return ["valroller"];
    };

    this.onShow = function () {

    };

    this.showClearFilterButton = function () {
        return true;
    };

    this.onRender = function (container, lang, typeName, values, cb, data) {

        this.cb = cb;
        this.container = container;
        this.lang = lang;

        this.value = values.length > 0 ? values[0] : { filterType: 1, filterValue: "" };

        this.renderWidget();
        this.loadCustomers();
        this.registerEvents();
    };
    this.renderWidget = function () {
        var html = '<p style="font-weight:bold;">Filtrelenicek Dereceyi Seçiniz :</p>\
                    <div id="cutomerlistDivID" style="width:220px;height:135px;overflow-y:auto;" class="grid-filter-type customerslist form-control"></div><br />\
                    <div class="grid-filter-buttons">\
                        <button type="button" class="btn btn-primary grid-apply" >' + this.lang.applyFilterButtonText + '</button>\
                    </div>';
        this.container.append(html);
    };

    this.loadCustomers = function () {
        var $this = this;
        $.post("/OyunProfilleri/ValorantRolGetirAjax", function (data) {
            $this.fillCustomers(data);
        });
    };

    this.fillCustomers = function (items) {
        var customerList = this.container.find(".customerslist");
        for (var i = 0; i < items.length; i++) {
            customerList.append('<input type = "checkbox" style="margin: 4px 8px 0;"' + (this.value.filterValue.indexOf(items[i]) > -1 ? 'checked="checked"' : '') + ' value="' + items[i] + '">' + items[i] + '</input><br />');
        }
    };

    this.registerEvents = function () {

        var customerList = this.container.find(".customerslist");

        var applyBtn = this.container.find(".grid-apply");

        var $context = this;

        applyBtn.click(function () {

            var type = 1;

            var value = "";

            $('.customerslist input:checked').each(function (index, element) {
                if (index == 0) {
                    value += element.value;
                }
                else {
                    value += "|" + element.value;
                }
            });

            var filterValues = [{ filterType: type, filterValue: value }];
            $context.cb(filterValues);
        });

        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        });
    };

}