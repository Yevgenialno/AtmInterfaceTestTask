let inputField = $('#pin')[0];

inputField.value = '';

let counter = 0;
$('#number-buttons >tbody>tr>td>button').on('click', (event) => {
    if (counter < 4) {
        counter++;
        inputField.value += event.target.innerText;
    }
})

$('#clear').on('click', () => {
    inputField.value = '';
    counter = 0;
})