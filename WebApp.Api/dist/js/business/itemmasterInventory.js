
var ItemMasterInventory = {
    init: function () {
        $.when(ItemMasterInventory.loadSite(), ItemMasterInventory.loadItemMasterInventory()).done(() => {
            ItemMasterInventory.registerEvent();
        })
        
    },
    registerEvent: function () {

        $('#frmItemmasterInventory').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                IMIQtyOnHand: {
                    required: true
                },
                IMIQtyAllocated: {
                    required: true
                },
                selectSiteId: {
                    required: true
                },
                selectItemMasterId: {
                    required: true
                }
            },
            messages: {
                //IMIQtyOnHand: "Vui lòng nhập IMIQtyOnHand",
                //IMIQtyAllocated: "Vui lòng nhập IMIQtyAllocated",
                //selectSiteId: "Vui lòng chọn site",
                //selectItemMasterId: "Vui lòng chọn Item Master"
            }
        });

        $('#buttonSubmitForm').off().on('click', function (event) {
            event.preventDefault();
            if ($('#frmItemmasterInventory').valid()) {
                var ItemMasterInventoryModel = {
                    IMIQtyOnHand: $("#txtIMIQtyOnHand").val(),
                    IMIQtyAllocated: $("#txtIMIQtyAllocated").val(),
                    IMISiteID: $("select[name='selectSiteId']").val(),
                    ItemMasterID: $("select[name='selectItemMasterId']").val()
                }
                
                $.ajax({
                    url: "/api/item-master-inventory/add",
                    type: "POST",
                    datatype: 'json',
                    async: true,
                    beforeSend: function () {
                        $("#spinner").addClass("spinner");
                    },
                    data: ItemMasterInventoryModel,
                    success: function (data) {
                        RXN.notify('Add Success', 'success');
                    },
                    complete: function () {
                        $("#spinner").removeClass("spinner");
                    }
                })
            };
        })
    },

    loadSite: function () {
        SiteModel = [];
        return $.ajax({
            url: "/api/site/getall",
            type: "get",
            datatype: 'json',
            async: true  ,
            beforeSend: function () {
                $("#spinner").addClass("spinner");
            },
            success: function (data) {
                $("select[name='selectSiteId'").html('');
                $.each(data, function (key, value) {
                    $("select[name='selectSiteId']").append(
                        "<option value=" + value.Id + ">" + value.Name + "</option>"
                    );
                });
            },
            complete: function () {
                $("#spinner").removeClass("spinner");
            }
        })
    },

    loadItemMasterInventory: function () {
        ItemMasterInventoryModel = [];
        return $.ajax({
            url: "/api/item-master/getall",
            type: "get",
            datatype: 'json',
            async: true,
            beforeSend: function () {
                $("#spinner").addClass("spinner");
            },
            success: function (data) {
                if (data.length > 0) {
                    $("select[name='selectItemMasterId'").html('');
                    $.each(data, function (key, value) {
                        $("select[name='selectItemMasterId']").append(
                            "<option value=" + value.Id + ">" + value.Id + "</option>"
                        );
                    });
                }
            },
            complete: function () {
                $("#spinner").removeClass("spinner");
            }
        });
    },
}
$(document).ready(function () {
    ItemMasterInventory.init();
});