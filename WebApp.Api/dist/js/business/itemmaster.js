var flagDiscard = false;
var ItemMaster = {
    init: function () {
        $.when(ItemMaster.loadItemMasterId(), ItemMaster.loadselectIMPrePackStyle()).done(() => {
            ItemMaster.registerEvent();
        })

    },
    registerEvent: function () {
        $('#frmItemmaster').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                IMPack: {
                    required: true
                },
                IMDescription: {
                    required: true
                },
                IMCostCenterCode: {
                    required: true
                },
                IMExpirationDate: {
                    required: true
                },
                IMUnitPrice: {
                    required: true
                },
                Width: {
                    required: true
                },
                Lenght: {
                    required: true
                },
                Height: {
                    required: true
                },
                selectIMPrePackStyle: {
                    required: true
                }
            },
            messages: {
                //IMPack: "Vui lòng nhập IMPack",
                //IMDescription: "Vui lòng nhập IMDescription",
                //IMCostCenterCode: "Vui lòng nhập IMCostCenterCode",
                //IMExpirationDate: "Vui lòng nhập IMExpirationDate",
                //IMUnitPrice: "Vui lòng nhập IMUnitPrice",
                //Width: "Vui lòng nhập Width",
                //Lenght: "Vui lòng nhập Lenght"
            }
        });

        $(".CheckIMIsPrePack .icheckbox_flat").click(function () {
            if ($('#CheckIMIsPrePack').is(':checked')) {
                $('#CheckIMIsPrePack').prop('checked', false);
                $(".CheckIMIsPrePack .icheckbox_flat").removeClass("checked");
            }
            else {
                $('#CheckIMIsPrePack').prop('checked', true);
                $(".CheckIMIsPrePack .icheckbox_flat").addClass("checked");
            }
        });

        $(".CheckIMIsHazardousMaterial .icheckbox_flat").click(function () {
            if ($('#CheckIMIsHazardousMaterial').is(':checked')) {
                $('#CheckIMIsHazardousMaterial').prop('checked', false);
                $(".CheckIMIsHazardousMaterial .icheckbox_flat").removeClass("checked");
            }
            else {
                $('#CheckIMIsHazardousMaterial').prop('checked', true);
                $(".CheckIMIsHazardousMaterial .icheckbox_flat").addClass("checked");
            }
        });

        $('#buttonSubmitForm').off().on('click', function (event) {
            event.preventDefault();
            if ($('#frmItemmaster').valid()) {
                var model = {
                    Id: $("select[name='selectId']").val(),
                    IMPack: $("#txtIMPack").val(),
                    IMDescription: $("#txtIMDescription").val(),
                    IMIsHazardousMaterial: $('#CheckIMIsHazardousMaterial').is(':checked'),
                    IMCostCenterCode: $("#txtIMCostCenterCode").val(),
                    IMExpirationDate: $("#txtIMExpirationDate").val(),
                    IMUnitPrice: $("#txtIMUnitPrice").val(),
                    IMWidth: $("#txtWidth").val(),
                    IMLength: $("#txtLenght").val(),
                    IMHeight: $("#txtHeight").val(),
                    IMIsPrePack: $('#CheckIMIsPrePack').is(':checked'),
                    IMPrePackStyle: $("select[name='selectIMPrePackStyle']").val(),
                    IMImageData: $("#TXTIMGIMImageData").val()
                }

                var file_data = $("#btnBroweLogo").prop("files")[0]; // Getting the properties of file from file field
                var form_data = new FormData(); // Creating object of FormData class
                form_data.append("file", file_data) // Appending parameter named file with properties of file_field to form_data

                let type = "POST";
                let url = "/api/item-master/add";

                if (model.Id !== "") {
                    {
                        type = "PUT";
                        url = "/api/item-master/update";
                    }
                }

                if (file_data === undefined) {
                    model.IMImageData = model.IMImageData;
                    ItemMaster.ChangeModel(model, type, url);
                } else {
                    if (!flagDiscard) {
                        $.when(ItemMaster.uploadImage(form_data)).done((data) => {
                            model.IMImageData = data;
                            ItemMaster.ChangeModel(model, type, url);
                        })
                    }
                    else {
                        model.IMImageData = "";
                        ItemMaster.ChangeModel(model, type, url);
                    }
                }

                
            };
        });

        $('#btnLogo').off().on('click', function () {
            $("#btnBroweLogo").click();
        });

        $("#txtWidth").change(function (event) {
            event.preventDefault();
            $("#txtCube").val(ItemMaster.caculateCube());
        });

        $("#txtLenght").change(function () {
            event.preventDefault();
            $("#txtCube").val(ItemMaster.caculateCube(event));
        });

        $("#txtHeight").change(function () {
            event.preventDefault();
            $("#txtCube").val(ItemMaster.caculateCube(event));
        });

        $("select[name='selectId'").change(function (event) {
            event.preventDefault();
            let selectId = $(this).val();
            if (selectId === "") { ItemMaster.resetFormMaintainance(); }
            else {
                $.when(ItemMaster.getById(selectId), ItemMaster.getByItemMasterInventory(selectId)).done(() => { });
            }
        });

        $("#btnBroweLogo").change(function () {
            ItemMaster.readURL(this);
            flagDiscard = false;
        });

        $("#btnDiscardLogo").off().on('click', function () {
            $('#IMGIMImageData').attr('src', "");
            flagDiscard = true;
        });
    },

    loadItemMasterId: function () {
        return $.ajax({
            url: "/api/item-master/getall",
            type: "get",
            datatype: 'json',
            async: true,
            beforeSend: function () {
                $("#spinner").addClass("spinner");
            },
            success: function (data) {
                $("select[name='selectId'").html('');
                $("select[name='selectId']").append(
                    "<option value=''>-- -</option>"
                );
                $.each(data, function (key, value) {
                    $("select[name='selectId']").append(
                        "<option value=" + value.Id + ">" + value.Id + "</option>"
                    );
                });
            },
            complete: function () {
                $("#spinner").removeClass("spinner");
            }
        })
    },

    loadselectIMPrePackStyle: function () {
        $("select[name='selectIMPrePackStyle'").html('');
        $("select[name='selectIMPrePackStyle']").append(
            "<option value='1'>Solid Style</option>"
        );
    },

    caculateCube: function () {
        return $("#txtWidth").val() * $("#txtLenght").val() * $("#txtHeight").val();
    },

    getById: function (Id) {
        $.ajax({
            url: "/api/item-master/getById/" + Id,
            type: "get",
            datatype: 'json',
            async: true,
            beforeSend: function () {
                $("#spinner").addClass("spinner");
            },
            success: function (data) {
                ItemMaster.getFormMaintainance(data);
            },
            complete: function () {
                $("#spinner").removeClass("spinner");
            }
        })
    },

    getByItemMasterInventory: function (Id) {
        $.ajax({
            url: "/api/item-master-inventory/getmuti/" + Id,
            type: "get",
            datatype: 'json',
            async: true,
            beforeSend: function () {
                $("#spinner").addClass("spinner");
            },
            success: function (response) {
                var template = $('#template-inventoryTable').html();
                var render = "";
                var On_Hand = 0;
                var On_Hand_pcs = 0;
                var Alocated = 0;
                var Alocated_pcs = 0;
                var Available = 0;
                var Available_pcs = 0;

                $.each(response, function (i, item) {
                    render += Mustache.render(template,
                        {
                            Location: item.NameSite,
                            On_Hand: item.IMIQtyOnHand,
                            On_Hand_pcs: item.IMIQtyOnHand * $("#txtIMPack").val(),
                            Alocated: item.IMIQtyAllocated,
                            Alocated_pcs: item.IMIQtyAllocated * $("#txtIMPack").val(),
                            Available: item.IMIQtyOnHand - item.IMIQtyAllocated,
                            Available_pcs: (item.IMIQtyOnHand - item.IMIQtyAllocated) * $("#txtIMPack").val()
                        });
                    On_Hand += item.IMIQtyOnHand;
                    On_Hand_pcs += item.IMIQtyOnHand * $("#txtIMPack").val();
                    Alocated += item.IMIQtyAllocated;
                    Alocated_pcs += item.IMIQtyAllocated * $("#txtIMPack").val();
                    Available += item.IMIQtyOnHand - item.IMIQtyAllocated;
                    Available_pcs += (item.IMIQtyOnHand - item.IMIQtyAllocated) * $("#txtIMPack").val();
                });

                $("#On_Hand").text(On_Hand);
                $("#On_Hand_pcs").text(On_Hand_pcs);
                $("#Alocated").text(Alocated);
                $("#Alocated_pcs").text(Alocated_pcs);
                $("#Available").text(Available);
                $("#Available_pcs").text(Available_pcs);

                if (render !== "")
                    $('#inventoryTable').html(render);
                else
                    $('#inventoryTable').html('You have no inventory in table');

            },
            complete: function () {
                $("#spinner").removeClass("spinner");
            }
        })
    },

    resetFormMaintainance: function () {
        //$("select[name='selectId']").val("");
        $("#txtIMPack").val("");
        $("#txtIMDescription").val("");
        $("#txtIMCostCenterCode").val("");
        $("#txtIMExpirationDate").val("");
        $("#txtIMUnitPrice").val("");
        $("#txtWidth").val("");
        $("#txtLenght").val("");
        $("#txtHeight").val("");
        $("#IMGIMImageData").attr("src", "/Content/img.jpg");
        $("#TXTIMGIMImageData").val("");
    },

    getFormMaintainance: function (data) {
        $("select[name='selectId']").val(data.Id);
        $("#txtIMPack").val(data.IMPack);
        $("#txtIMDescription").val(data.IMDescription);
        $("#txtIMCostCenterCode").val(data.IMCostCenterCode);
        $("#txtIMExpirationDate").val(ItemMaster.parseJsonDate(data.IMExpirationDate));
        $("#txtIMUnitPrice").val(data.IMUnitPrice);
        $("#txtWidth").val(data.IMWidth);
        $("#txtLenght").val(data.IMLength);
        $("#txtHeight").val(data.IMHeight);
        $("#IMGIMImageData").attr("src", data.IMImageData);
        $("#TXTIMGIMImageData").val(data.IMImageData);

        if (data.IMIsHazardousMaterial) {
            $('#CheckIMIsHazardousMaterial').prop('checked', true);
            $(".CheckIMIsHazardousMaterial .icheckbox_flat").addClass("checked");
        }
        else {
            $('#CheckIMIsHazardousMaterial').prop('checked', false);
            $(".CheckIMIsHazardousMaterial .icheckbox_flat").removeClass("checked");
        }

        if (data.IMIsPrePack) {
            $('#CheckIMIsPrePack').prop('checked', true);
            $(".CheckIMIsPrePack .icheckbox_flat").addClass("checked");
        }
        else {
            $('#CheckIMIsPrePack').prop('checked', false);
            $(".CheckIMIsPrePack .icheckbox_flat").removeClass("checked");
        }
    },

    ChangeModel: function (model, type, url) {
        $.ajax({
            url: url,
            type: type,
            datatype: 'json',
            async: true,
            beforeSend: function () {
                $("#spinner").addClass("spinner");
            },
            data: model,
            success: function (data) {
                RXN.notify('Action Success', 'success');
                if (model.Id !== "") {
                    ItemMaster.getById(model.Id);
                }
                else {
                    ItemMaster.loadItemMasterId();
                }
            },
            complete: function () {
                $("#spinner").removeClass("spinner");
                ItemMaster.resetFormMaintainance();
            }
        })
    },

    readURL: function (input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#IMGIMImageData').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    },

    uploadImage: function (form_data) {
        return $.ajax({
            url: "/api/item-master/saveImage", // Upload Script
            dataType: 'json',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data, // Setting the data attribute of ajax with file_data
            type: 'post',
            success: function (dataImage) {
            }
        });
    },

    parseJsonDate: function (datetime) { //trả về json ngày tháng năm
        var dmn;
        if (datetime === null || datetime === "") {
            dmn = new Date();
        }
        else {
            dmn = datetime;
        }
        var formattedDate = new Date(dmn);
        var d = formattedDate.getDate();
        var m = formattedDate.getMonth();
        m += 1;  // JavaScript months are 0-11
        var y = formattedDate.getFullYear();

        return y + "-" + m + "-" + d;
    }

}
$(document).ready(function () {
    ItemMaster.init();
});