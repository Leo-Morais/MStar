import styles from './Select.module.css';

function Select({ text, name, options, handleOnChange, value }) {
    return (
        <div className={styles.form_control}>
            <label htmlFor={name}>{text}:</label>
            <select 
                name={name} 
                id={name} 
                onChange={handleOnChange} 
                value={value || ''} // Mantém o valor controlado pelo estado
            >
                <option value="">Selecione uma opção</option>
                {options.length > 0 ? (
                    options.map((option) => (
                        <option value={option.id} key={option.id}>
                            {option.name}
                        </option>
                    ))
                ) : (
                    <option disabled>Carregando opções...</option>
                )}
            </select>
        </div>
    );
}

export default Select;
