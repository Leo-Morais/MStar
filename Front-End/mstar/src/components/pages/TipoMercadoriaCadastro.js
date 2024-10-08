import TipoMercadoriaForm from '../project/TipoMercadoriaForm'
import styles from './NewProjects.module.css'

function TipoMercadoriaCadastro(){
    return (
        <div className={styles.newproject_container}>
            <h1>TipoMercadoria Cadastro</h1>
            <p>Crie seu projeto para depois adicionar aos seriços</p>
            <TipoMercadoriaForm btnText="Enviar"/>
        </div>
    )
}

export default TipoMercadoriaCadastro