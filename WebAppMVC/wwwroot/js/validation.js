const formErrorHandler = (element, validationResult) => {
    let spanElemet = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.target.classList.remove('input-validation-error')
        spanElemet.classList.remove('field-validation-error')
        spanElemet.classList.add('field-validation-valid')
        spanElement.innerHTML = ''
    }
    else {
        element.target.classList.add('input-validation-error')
        spanElemet.classList.add('field-validation-error')
        spanElemet.classList.remove('field-validation-valid')
        spanElement.innerHTML = element.target.dataset.valRequired

    }
}


const textValidator = (element, minLenght = 2) => {
    if (element.value.lenght >= minLenght) {
        form.formErrorHandler(element, true)
    }
    else {
        form.formErrorHandler(element, false)
    }

}

const emailValidator = (element) => {
    const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/
    formErrorHandler(element, regEx.test(element.value))
}

const passwordValidator = (element) => {
    if (element.dataset.valEqualToOther !== undefined) {
        let password = document.getElementsByName(element.dataset.valEqualToOther.replace('*', 'Form'))[0].value

        if (element.value === password)
            form.formErrorHandler(element, true)
        else
            form.formErrorHandler(element, false)
            
    }
    else {
        const regEx = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"
        formErrorHandler(element, regEx.test(element.value))
    }

}

const checkboxValidator = (element) => {
    if (element.checked)
        form.formErrorHandler(element, true)
    else
        formErrorHandler(element, false)
}


let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.target)
            })
        }
        else {

            input.addEventListener('keyup', (e) => {

                switch (e.target.type) {

                    case 'text':
                        textValidator(e.target)
                        break;

                    case 'email':
                        emailValidator(e.target)
                        break;

                    case 'password':
                        passwordValidator(e.target)
                }
            })
        }
    }
})
