import MercadoriaForm from '../project/MercadoriaForm'
import styles from './NewProjects.module.css'

function MercadoriaCadastro() {
    return (
        <div className={styles.newproject_container}>
            <h1>Cadastro de Mercadoria</h1>
            <p>Cadastre uma nova mercadoria e associe-a a um tipo de mercadoria existente</p>
            <MercadoriaForm btnText="Enviar" />
        </div>
    )
}

export default MercadoriaCadastro
