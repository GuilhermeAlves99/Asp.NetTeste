$(document).ready(function () {
    const gridBeneficiarios = $('#gridBeneficiarios');

    if (gridBeneficiarios.length) {
        gridBeneficiarios.jtable({
            title: 'Beneficiários',
            paging: true, 
            pageSize: 5, 
            sorting: true, 
            defaultSorting: 'Nome ASC', 
            actions: {
                listAction: urlBenefList, 
            },
            fields: {
                CPF: {
                    title: 'CPF',
                    width: '50%'
                },
                Nome: {
                    title: 'Nome',
                    width: '35%'
                },
                Alterar: {
                    title: '',
                    width: '15%',
                    sorting: false,
                    display: function (data) {
                        return '<button onclick="window.location.href=\'' + urlAlteraBenef + '/' + data.record.Id + '\'" class="btn btn-primary btn-sm">Alterar</button>';
                    }
                }
            }
        });
        gridBeneficiarios.jtable('load');
    }
});
