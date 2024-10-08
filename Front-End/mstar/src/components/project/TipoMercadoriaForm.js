import { useState } from 'react'
import Input from '../form/Input'
import SubmitButton from '../form/SubmitButton'
import styles from './TipoMercadoriaForm.module.css'

function TipoMercadoriaForm({ btnText }) {
    const [tipo, setTipo] = useState('')

    const handleSubmit = (e) => {
        e.preventDefault()
        
        const newTipoMercadoria = {
            tipo: tipo
        }

        fetch("https://localhost:7116/api/v1/TipoMercadoria", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newTipoMercadoria),
        })
        .then((resp) => resp.json())
        .then((data) => {
            console.log('Tipo de Mercadoria cadastrado com sucesso:', data)
            // Aqui você pode redirecionar ou limpar o formulário após o sucesso
        })
        .catch((err) => console.log('Erro ao cadastrar tipo de mercadoria:', err))
    }

    return (
        <form onSubmit={handleSubmit} className={styles.form}>
            <Input 
                type="text" 
                text="Tipo de Mercadoria" 
                name="tipo" 
                placeholder="Insira o tipo de mercadoria" 
                handleOnChange={(e) => setTipo(e.target.value)}
                value={tipo}
            />
            <SubmitButton text= {btnText} />
        </form>
    )
}

export default TipoMercadoriaForm
