let inputField = $('#amount')[0];

inputField.value = '';

$('#number-buttons >tbody>tr>td>button').on('click', (event) => {
    if (event.target.innerText != ',' || !inputField.value.includes(',')) {
        inputField.value += event.target.innerText;
    }
})

$('#clear').on('click', () => {
    inputField.value = '';
})