let inputField = $('#card-number')[0];

let counter = 0;
$('#number-buttons >tbody>tr>td>button').on('click', (event) => {
    if (counter < 16) {
        if (counter != 0 && counter % 4 == 0) {
            inputField.value += '-';
        }

        counter++;
        inputField.value += event.target.innerText;
    }
})

$('#clear').on('click', () => {
    inputField.value = '';
    counter = 0;
})